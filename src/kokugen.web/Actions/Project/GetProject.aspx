<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Project.GetProject" MasterPageFile="~/Shared/Site.Master"%>
<%@ Import Namespace="FubuMVC.Core.Urls"%>
<%@ Import Namespace="Kokugen.Web.Actions.Project"%>
<asp:Content ID="ProjectListHead" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="ProjectListContent" ContentPlaceHolderID="mainContent" runat="server">
<body>
    <%= this.DisplayFor(m => m.Project.Name) %>
</body>
    

</asp:Content>