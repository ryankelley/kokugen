$(document).ready(function()
{  	
	$("#settings_form").validate({
		rules: {
			kokugen_url_input: { required: true },
			username_input: { required: true },
			password_input: { required: true }
		}
	});
    
    $("#kokugen_url_input").val(System.Gadget.Settings.read("KokugenUrl"));
	$("#username_input").val(System.Gadget.Settings.read("UserName"));
	$("#password_input").val(System.Gadget.Settings.read("Password"));
                
});

System.Gadget.onSettingsClosing = function(event)
{	
    if (event.closeAction == event.Action.commit && $("#settings_form").valid() == true)
    {   
		System.Gadget.Settings.write("KokugenUrl",$("#kokugen_url_input").val());
		System.Gadget.Settings.write("UserName",$("#username_input").val());
		System.Gadget.Settings.write("Password",$("#password_input").val());
        event.cancel = false;
    }
	else
	{
		event.cancel = true;
	}
}

//