$(document).ready(function()
{  	
	$("#settings_form").validate({
		rules: {
			kokugen_url_input: { required: true },
			username_input: { required: true },
			password_input: { required: true }
		}
	});
    
    $("#kokugen_url_input").val(System.Gadget.Settings.readString("KokugenUrl"));
	$("#username_input").val(System.Gadget.Settings.readString("UserName"));
	$("#password_input").val(System.Gadget.Settings.readString("Password"));
                
});

System.Gadget.onSettingsClosing = function(event)
{	
    if (event.closeAction == event.Action.commit && $("#settings_form").valid() == true)
    {   
		System.Gadget.Settings.writeString("KokugenUrl",$("#kokugen_url_input").val());
		System.Gadget.Settings.writeString("UserName",$("#username_input").val());
		System.Gadget.Settings.writeString("Password",$("#password_input").val());
        event.cancel = false;
    }
	else
	{
		event.cancel = true;
	}
}

//