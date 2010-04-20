<%@ Control Language="C#" Inherits="Kokugen.Web.Actions.Card.CardTask_Item" %>
<%@ Import Namespace="Kokugen.Core.Services" %>

<li id="<%= Model.Id %>"><%= this.EditInPlace(x => x.Description) %><span class="task-data <% if(Model.CompletedDate == null) { Response.Write("hiddden");} %>"><%=this.DisplayFor(m=> m.CompletedDate) %><%= this.DisplayFor(m => m.UserName) %></span></li>
