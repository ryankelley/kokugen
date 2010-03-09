<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Project.ProjectForm" MasterPageFile="~/Shared/Site.Master"%>
<%@ Import Namespace="Kokugen.Web.Conventions"%>
<%@ Import Namespace="Kokugen.Web.Actions.Project"%>
<asp:Content ID="THISCONTENTAREAID" ContentPlaceHolderID="mainContent" runat="server">
<div id="project-form">
<%= this.FormFor(new AddProjectModel()) %>

<<<<<<< HEAD
<%= this.InputFor(x => x.Project.Description) %>

<%= this.InputFor(x => x.Project.Company.Name) %>

<p>
    <input type = "submit" value = "Add" />
</p>

=======
<%= this.Edit(x => x.Project.Name) %>

<%= this.Edit(x => x.Project.Description) %>
>>>>>>> e095e70fb01749cbcb98b37e3b1541eb8be0f129
</form>
</div>
</asp:Content>