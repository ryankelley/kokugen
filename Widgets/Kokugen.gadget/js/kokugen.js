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
});

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

	}
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
		
}

function afterProjectsLoad(response) {

    if ($("#project_select").children().length <= 1) {

        if (response.Success) {

            for (var i in response.Item) {
                var item = '<option value="' + response.Item[i].Id + '">' + response.Item[i].Name + '</option>';

                $("#project_select").append(item);
            }
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
            CurrentTimeId = ""
        }
        updateScreenAfterSaveTask();
    });


}


