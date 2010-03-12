<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Project.List" MasterPageFile="~/Shared/Site.Master"%>
<%@ Import Namespace="Kokugen.Web.Actions.Project"%>
<asp:Content ID="ProjectListContent" ContentPlaceHolderID="mainContent" runat="server">
<body>
    <%= this.PartialForEach(x => x.Projects).Using<ProjectItem_Control>() %>
    
    <% this.Partial(new ProjectFormModel()); %>
</body>
</asp:Content>