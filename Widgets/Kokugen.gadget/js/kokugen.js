System.Gadget.settingsUI = "settings.html";
System.Gadget.onSettingsClosed = SettingsClosed;
var clicknum = 0;
KokugenUrl = "";
UserName = "";
Password = "";

isConnected = false;

$(document).ready(function(){
	$("#slider_button").click(function(){
		System.Gadget.Flyout.show = !System.Gadget.Flyout.show;
	});
	
	KokugenUrl = System.Gadget.Settings.readString("KokugenUrl");
	UserName = System.Gadget.Settings.readString("UserName");
	Password = System.Gadget.Settings.readString("Password");
	updateStatus(isConnected);	
});

function updateStatus(connected){
    if (connected == true) {
        $("#project_select").removeAttr("disabled");		
    }
    else {
        $("#project_select").attr("disabled", true);
    }
}

//Try to connect using the url and credentials provided.
function connect(){
	//login(setIsConnected);
	if(isConnected)
	{	    		
	    afterConnected();
	}
	updateStatus(isConnected);
}

function setIsConnected(response){
	if(response && response.Success)
	{
		isConnected = true;
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
		connect();		
	}	
}

function afterConnected() {    
    login(setIsConnected);
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
        for (var i in response.Item) {
            var item = '<option value="' + response.Item[i].Id + '">' + response.Item[i].Name + '</option>';

            $("#card_select").append(item);
        }
    }
}

function afterTasksLoad(response) {

    if (response.Success) {
        for (var i in response.Item) {
            var item = '<option value="' + response.Item[i].Id + '">' + response.Item[i].Name + '</option>';

            $("#task_select").append(item);
        }
    }
}

(function ($) {
    $.fn.selected = function (fn) {
        return this.each(function () {
            var clicknum = 0;
            $(this).click(function () {
                clicknum++;
                if (clicknum == 2) {
                    clicknum = 0;
                    fn();
                }
            });
        });
    }
})(jQuery);

