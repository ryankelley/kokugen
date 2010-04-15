<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.TimeRecord.ProjectTimeRecordForm"%>
<%@ Import Namespace="Kokugen.Web.Actions.TimeRecord"%>
<%@ Import Namespace="FubuMVC.Core.Urls"%>
<%@ Import Namespace="Kokugen.Web.Conventions"%>


<div id="timerecord-form-container" class="hidden">
<%= this.FormFor(new AddTimeRecordModel() {}).Id("time-record-form")%>
    <%= this.Edit(x => x.TimeRecord.Description) %>
    <%= this.Edit(x =>x.TaskId) %>
    <%= this.InputFor( x => x.ProjectId) %>
    
    
<%= this.EndForm() %>
   
</div>
<script type="text/javascript">

    function closeDialog(response) {
    $("#timerecord-form-container").dialog('close');
        appendTimeRecordToList(response.Item);

        
        // would want to update list here too
    }
    function validateAndSave() {
        var options = {
            success: closeDialog,  // post-submit callback 
            type: 'post',        // 'get' or 'post', override for form's 'method' attribute 
            dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type) 
            clearForm: true        // clear all form fields after successful submit 
        };
        var isValid = $("#time-record-form").valid();

        if (isValid) {
            $("#time-record-form").ajaxSubmit(options);
        }
    }

    $(document).ready(function () {
        $("#time-record-form").validate({ errorClass: "error" });
        $("#timerecord-form-container").dialog({ title: "Add Time Record", autoOpen: false, buttons: { "Save": validateAndSave} });
        $("#timerecord-form-container").submit(validateAndSave);
    });
</script>
