<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.TaskCategory.TaskForm"%>
<%@ Import Namespace="Kokugen.Web.Actions.TaskCategory"%>
<%@ Import Namespace="FubuMVC.Core.Urls"%>
<%@ Import Namespace="Kokugen.Web.Conventions"%>


<div id="task-form-container" class="hide">
<%= this.FormFor(new AddTaskModel()) %>
    <%= this.Edit(x => x.Task.Name) %>
    
</form>
   
</div>
<script type="text/javascript">

    function closeDialog(response) {
        appendTaskToList(response.Item);

        $("#task-form-container").dialog('close');
        // would want to update list here too
    }
    function validateAndSave() {
        var options = {
            success: closeDialog,  // post-submit callback 
            type: 'post',        // 'get' or 'post', override for form's 'method' attribute 
            dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type) 
            clearForm: true        // clear all form fields after successful submit 
        };
        var isValid = $("#task-form").valid();

        if (isValid) {
            $("#task-form").ajaxSubmit(options);
        }
    }

    $(document).ready(function() {
        $("#task-form").validate({ errorClass: "error" });
        $("#task-form-container").dialog({ title: "Add Task", autoOpen: false, buttons: { "Save": function() { validateAndSave(); } } });
    });
</script>
