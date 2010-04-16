<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Company.CompanyForm"%>
<%@ Import Namespace="Kokugen.Web.Conventions"%>
<%@ Import Namespace="Kokugen.Web.Actions.Company"%>

<div id="company-form-container" class="hide">
<%= this.FormFor(new AddCompanyInput()).Id("company-form") %>
       
    <%= this.Edit(x => x.Company.Name).Id("company-edit-form-name") %>
    <%= this.Edit(x => x.Company.Address.StreetLine1)%>
    <%= this.Edit(x => x.Company.Address.StreetLine2)%>
    <%= this.Edit(x => x.Company.Address.City)%>
    <%= this.Edit(x => x.Company.Address.State)%>
    <%= this.Edit(x => x.Company.Address.ZipCode)%>
    <%= this.InputFor(x => x.Company.Id).Id("company-edit-form-ProjectId") %>
<%= this.EndForm() %>


   
</div>

<script type="text/javascript">

    function closeDialog(response) {

        $("#company-form-container").dialog('close');

        var elements = document.getElementById("company-form").elements;
        for (var i in elements) {
            elements[i].value = '';
        }

        addCompanyToList(response.Item);
    }
    
    function validateAndSave() {
        var options = {
            success: closeDialog,  // post-submit callback 
            type: 'post',        // 'get' or 'post', override for form's 'method' attribute 
            dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type) 
            clearForm: true        // clear all form fields after successful submit 
        };
        var isValid = $("#company-form").valid();

        if (isValid) {
            $("#company-form").ajaxSubmit(options);
        }
        
    }

    $(document).ready(function() {
        $("#company-form").validate({ errorClass: "error" });

        $("#company-form-container").dialog({ title: "Add Company", autoOpen: false, width: 450, buttons: { "Cancel": function() { $("#company-form-container").dialog('close'); }, "Save": function() { validateAndSave(); } } });


        // bind form using 'ajaxForm'
        //$('#project-form').ajaxForm(options);
    });
</script>
