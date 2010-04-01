<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.TimeRecord.TimeRecordForm"%>
<%@ Import Namespace="Kokugen.Web.Actions.TimeRecord"%>
<%@ Import Namespace="FubuMVC.Core.Urls"%>
<%@ Import Namespace="Kokugen.Web.Conventions"%>


<div id="task-form-container" class="hide">
<%= this.FormFor(new AddTimeRecordModel()) %>
    <%= this.Edit(x => x.TimeRecord.Description %>
    <%= this.Edit(x =>x.TimeRecord.Task) %>
    
</form>
   
</div>
<script type="text/javascript">

    function closeDialog(response) {
        appendTimeRecordToList(response.Item);

        $("#timerecord-form-container").dialog('close');
        // would want to update list here too
    }
    function validateAndSave() {
        var options = {
            success: closeDialog,  // post-submit callback 
            type: 'post',        // 'get' or 'post', override for form's 'method' attribute 
            dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type) 
            clearForm: true        // clear all form fields after successful submit 
        };
        var isValid = $("#mainForm").valid();

        if (isValid) {
            $("#mainForm").ajaxSubmit(options);
        }
    }

    $(document).ready(function() {
    $("#mainForm").validate({ errorClass: "error" });
    $("#timerecord-form-container").dialog({ title: "Add Time Record", autoOpen: false, buttons: { "Save": validateAndSave} });
    });
</script>
