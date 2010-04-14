<%@ Control Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Project.ProjectMenu_Item" %>
<%@ Import Namespace="Kokugen.Web.Actions.Project" %>
<li><%= this.LinkTo(new GetProjectModel{Id = Model.Id}).Text(Model.Name) %></li>