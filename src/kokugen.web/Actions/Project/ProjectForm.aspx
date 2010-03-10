<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Project.ProjectForm"%>
<%@ Import Namespace="FubuMVC.Core.Urls"%>
<%@ Import Namespace="Kokugen.Web.Conventions"%>
<%@ Import Namespace="Kokugen.Web.Actions.Project"%>

<div id="project-form-container" class="hide">
<%= this.FormFor(new AddProjectModel()).Id("project-form") %>

<%= this.Edit(x => x.Project.Name) %>

<%= this.Edit(x => x.Project.Description) %>

<%= this.Edit(x => x.CompanyId) %>

<p>
    <input type = "submit" value = "Add" id = "add-project-button"/>
</p>

</form>
   
</div>
<script type="text/javascript">
    $(document).ready(function() {
    $("#project-form").validate();
    });
</script>
