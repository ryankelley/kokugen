<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Project.ProjectForm" MasterPageFile="~/Shared/Site.Master"%>
<%@ Import Namespace="Kokugen.Web.Actions.Project"%>
<asp:Content ID="THISCONTENTAREAID" ContentPlaceHolderID="mainContent" runat="server">
<div id="project-form">
<%= this.FormFor(new AddProjectModel()) %>
<%= this.InputFor(x => x.Project.Name) %>

<%= this.InputFor(x => x.Project.Description) %>

<%= this.InputFor(x => x.Project.Company.Name) %>

<p>
    <input type = "submit" value = "Add" />
</p>

</form>
</div>
</asp:Content>