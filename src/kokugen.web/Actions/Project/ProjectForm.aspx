<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Project.ProjectForm"%>
<%@ Import Namespace="FubuMVC.Core.Urls"%>
<%@ Import Namespace="Kokugen.Web.Conventions"%>
<%@ Import Namespace="Kokugen.Web.Actions.Project"%>

<div id="project-form-container" class="hide">
<%= this.FormFor(new AddProjectModel()).Id("project-form") %>

<%= this.Edit(x => x.Project.Name) %>

<%= this.Edit(x => x.Project.Description) %>

<%= this.Edit(x => x.CompanyId) %>

</form>
   
</div>
<script type="text/javascript">

    function closeDialog(response) {
        
        alert(response.Item);
        $("#project-form-container").dialog('close');
        // would want to update list here too
    }
    function validateAndSave() {
        var options = {
            success: closeDialog,  // post-submit callback 
            type: 'post',        // 'get' or 'post', override for form's 'method' attribute 
            dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type) 
            clearForm: true        // clear all form fields after successful submit 
        };
        var isValid = $("#project-form").valid();

        if (isValid) {
            $("#project-form").ajaxSubmit(options);
        }
        
    }

    $(document).ready(function() {
        $("#project-form").validate({ errorClass: "ui-state-error" });
        

        $("#project-form-container").dialog({ title: "Add Project", autoOpen: false, buttons: { "Save": function() { validateAndSave(); } } });


        // bind form using 'ajaxForm'
        //$('#project-form').ajaxForm(options);
    });
</script>
