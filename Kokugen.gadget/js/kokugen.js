System.Gadget.settingsUI = "settings.html";
System.Gadget.Flyout.file = "flyout.html";
System.Gadget.Flyout.onShow = showFlyout;
System.Gadget.Flyout.onHide = hideFlyout;
System.Gadget.onSettingsClosed = SettingsClosed;

KokugenUrl = "";
UserName = "";
Password = "";

isConnected = false;

$(document).ready(function(){
	$("#slider_button").click(function(){
		System.Gadget.Flyout.show = !System.Gadget.Flyout.show;
	});
	
	KokugenUrl = System.Gadget.Settings.read("KokugenUrl");
	UserName = System.Gadget.Settings.read("UserName");
	Password = System.Gadget.Settings.read("Password");
	connect();
	updateStatus(isConnected);
});

function updateStatus(connected){
	if(connected == true)
	{
		$("#project_select").removeAttr("disabled");
	}
	else
	{
		$("#project_select").attr("disabled", true);
	}
}

//Try to connect using the url and credentials provided.
function connect(){
	if(KokugenUrl != "")
	{
		isConnected = true;
	}
	else
	{
		isConnected = false;
	}
}

//Show the flyout by changing the css class.
function showFlyout()
{
	$("#slider_button").removeClass("slider_button_closed");
	$("#slider_button").addClass("slider_button_open");
}

//Hide the flyout by changing the css class.
function hideFlyout()
{
	$("#slider_button").addClass("slider_button_closed");
	$("#slider_button").removeClass("slider_button_open");
}

function SettingsClosed(event)
{
    // User hits OK on the settings page.
    if (event.closeAction == event.Action.commit)
    {
		$("#start_stop_button").addClass("stop_button");
		$("#start_stop_button").removeClass("start_button");
		
		KokugenUrl = System.Gadget.Settings.read("KokugenUrl");
		UserName = System.Gadget.Settings.read("UserName");
		Password = System.Gadget.Settings.read("Password");
		connect();
		
	}
    // User hits Cancel on the settings page.
    else if (event.closeAction == event.Action.cancel)
    {
		$("#start_stop_button").addClass("start_button");
		$("#start_stop_button").removeClass("stop_button");		
    }
	updateStatus(isConnected);
}
