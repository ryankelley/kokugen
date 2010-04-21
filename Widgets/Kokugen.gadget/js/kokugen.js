System.Gadget.settingsUI = "settings.html";
System.Gadget.onSettingsClosed = SettingsClosed;
var clicknum = 0;

KokugenUrl = "";
UserName = "";
Password = "";

var CurrentUserId = "";
var CurrentTimeId = {};
isLoggedIn = false;

$(document).ready(function(){
    $(".slide-button").click(function () {
		if(isLoggedIn) {
				
		System.Gadget.Flyout.show = !System.Gadget.Flyout.show;
		}
	});
	
	KokugenUrl = System.Gadget.Settings.readString("KokugenUrl");
	UserName = System.Gadget.Settings.readString("UserName");
	Password = System.Gadget.Settings.readString("Password");
	updateStatus(isLoggedIn);

	$('#stop-btn').click(function () { stopTask(); });
	$('#start-btn').click(function () { if (isLoggedIn) { startNewTask(); } });
	$('#save-btn').click(function () { completeTask(); });

	$('#clock-in').click(function () { clockIn(); });
	$('#clock-out').click(function () { clockOut(); });
	
	if(debug) {
		$('body').height(350);
		$("#debug").hide();
	}
	
});

function debugWrite(text) {
	if(debug) {
		$('#debug').append(text + '\n');
	}
}

function changeHeight(height) {
if(debug) {
		$('body').height(height + 125);
	}
	else { 
		$('body').height(height);
	}
}

function updateStatus(connected){
    if (isLoggedIn) {
        //$("#project_select").removeAttr("disabled");		
    }
    else {
        //$("#project_select").attr("disabled", true);
    }
}


function setIsConnected(response){
	if(response && response.Success) {
	    CurrentUserId= response.Item;
	    System.Gadget.Settings.write("KokugenUserId", response.Item);

	    isLoggedIn = true;
	    afterConnected();
	    $("#status").html("Logged In");
	    $("#status").removeClass("out").addClass("in");
		$(".server").html(KokugenUrl);
	}
	else { $("#status").removeClass("in").addClass("out"); }
}

function SettingsClosed(event)
{
    // User hits OK on the settings page.
    if (event.closeAction == event.Action.commit)
    {
		KokugenUrl = System.Gadget.Settings.read("KokugenUrl");
		UserName = System.Gadget.Settings.read("UserName");
		Password = System.Gadget.Settings.read("Password");
		login(setIsConnected);	
	}	
}

function afterConnected() {    
	loadProjects(afterProjectsLoad);
	loadTaskList(afterTasksLoad);
	$("#refreshmain").click(function () {
	    refreshBoxes();
	});
}

function afterProjectsLoad(response) {

    
	if (response.Success) {
	debugWrite('Clearing Projects');
	$("#project_select").children().each(function() { $(this).remove();});
		for (var i in response.Item) {
			var item = '<option value="' + response.Item[i].Id + '">' + response.Item[i].Name + '</option>';

			$("#project_select").append(item);
		}
	}
    

    $("#project_select").change(function () {
        var value = $("#project_select option:selected").val();

        loadCardList(value,afterCardsLoad);
    });
}

function afterCardsLoad(response) {
    if (response.Success) {
        $("#card_select").children().each(function () { $(this).remove(); });
        for (var i in response.Item) {
            var item = '<option value="' + response.Item[i].Id + '">' + response.Item[i].Name + '</option>';

            $("#card_select").append(item);
        }
    }
}

function afterTasksLoad(response) {

    if (response.Success) {
	$("#task_select").children().each(function () { $(this).remove(); });
        for (var i in response.Item) {
            var item = '<option value="' + response.Item[i].Id + '">' + response.Item[i].Name + '</option>';

            $("#task_select").append(item);
        }
    }
}

function submitStartTask() {
    var data = new TimeRecordData();

    data.UserId = CurrentUserId;
    data.TaskId = $("#task_select option:selected").val();
    data.ProjectId = $("#project_select option:selected").val();
    data.CardId = $("#card_select option:selected").val();
	
		if(data.TaskId == "" || data.TaskId == null ||
 	data.ProjectId =="" || data.ProjectId =="")
	{ return; }
    
    startTimeRecord(data, function (response) {
        if (response && response.Success) {
            CurrentTimeId = response.Item.Id;
        }
        updateScreenAfterStartTask();
        $("#description").html(CurrentTimeId);
    });


}

function submitSaveTask() {
    var data = new TimeRecordData();

    data.Id = CurrentTimeId;
    data.Duration = $("#worked_time_text").val();
    data.Billable = $("#billable_time_text").val();
    data.Description = $("#description").val();

    stopTimeRecord(data, function (response) {
        if (response && response.Success) {
            CurrentTimeId = "";
        }
        updateScreenAfterSaveTask();
    });


}

function refreshBoxes() {
    if (isLoggedIn) {
        loadProjects(afterProjectsLoad);
        loadTaskList(afterTasksLoad);
    }

}

function zeroPad(num, count) {
    var numZeropad = num + '';
    while (numZeropad.length < count) {
        numZeropad = "0" + numZeropad;
    }
    return numZeropad;
}

function formatTimeSpan(milliseconds) {
    var seconds = milliseconds / 1000;
    var minutes = parseInt(seconds / 60);
    var hours = parseInt(minutes / 60);
    var printMinutes = parseInt(minutes % 60);
    var printSeconds = parseInt(seconds % 60);

    return hours + ':' + zeroPad(printMinutes, 2) + ':' + zeroPad(printSeconds, 2);
}

var timer;
var startTime = new Date();
function startNewTask(event) {
    submitStartTask();
}

function updateScreenAfterStartTask() {

    startTime = new Date();


    $('#textField').val(formatTimeSpan(new Date() - startTime));

    timer = setInterval(function () {
        $('#textField').val(formatTimeSpan(new Date() - startTime));
    }, 1000);
    $('#start-btn').hide();
    $('#stop-btn').show();

    $("select").each(function () { $(this).hide(); });
    changeHeight(141);
}

function updateDuration() {
    var milliseconds = new Date() - startTime;
    var seconds = milliseconds / 1000;
    var output = (seconds / 3600);
    $('#worked_time_text').val(output);

}
function stopTask(event) {
    clearInterval(timer);

    updateDuration();


    $('#stop-btn').hide();
    $('#save-btn').show();

    changeHeight(295);
    $('#timerecord-info').show();
}

function completeTask() {
    // make server call
    submitSaveTask();
}

function updateScreenAfterSaveTask() {
	$("#worked_time_text").val('');
    $("#billable_time_text").val('');
    $("select").each(function () { $(this).show(); });
    $('#save-btn').hide();
    $('#start-btn').show();
    $('#timerecord-info').hide();
    changeHeight(225);
}

function clockOut() {
    // make server call

    $('#clock-in').show();
    $('#clock-out').hide();
}

function clockIn() {
    // make server cal


    $('#clock-in').hide();
    $('#clock-out').show();
}


