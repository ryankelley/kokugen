<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Project.ProjectForm" MasterPageFile="~/Shared/Site.Master"%>
<%@ Import Namespace="Kokugen.Web.Conventions"%>
<%@ Import Namespace="Kokugen.Web.Actions.Project"%>
<asp:Content ID="THISCONTENTAREAID" ContentPlaceHolderID="mainContent" runat="server">
<div id="project-form">
<%= this.FormFor(new AddProjectModel()) %>

<%= this.Edit(x => x.Project.Name) %>

<%= this.Edit(x => x.Project.Description) %>
</form>
</div>
</asp:Content>