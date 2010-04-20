System.Gadget.Flyout.file = "flyout.html";
System.Gadget.Flyout.onShow = showFlyout;
System.Gadget.Flyout.onHide = hideFlyout;


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


