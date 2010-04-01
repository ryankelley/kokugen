<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Project.GetProject" MasterPageFile="~/Shared/Site.Master"%>
<%@ Import Namespace="Kokugen.Web.Actions.Board"%>
<%@ Import Namespace="FubuMVC.Core.Urls"%>
<%@ Import Namespace="Kokugen.Web.Actions.Project"%>
<asp:Content ID="ProjectListHead" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="ProjectListContent" ContentPlaceHolderID="mainContent" runat="server">
<body>
<div id="Project Name"><h2><%= this.DisplayFor(m => m.Project.Name) %></h2></div>

<%= this.PartialForEach(p => p.TimeRecords).Using<TimeRecord_Control>() %>
    
    
   <div id ="Board Link"><%= this.LinkTo(new BoardConfigurationModel{ Id = Model.Project.Id}).Text("Configure Board Columns") %></div> 
</body>
    

</asp:Content>