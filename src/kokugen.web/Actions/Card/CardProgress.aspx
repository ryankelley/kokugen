<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Card.CardProgress" %>
<ul class="card-progress">
<%
    var progressStopped = false;
    foreach (var col in Model.Columns)
{
  
      %>
      <li class="<%= progressStopped ? "grey" : "green" %>"><%= col.Name %></li>
      <%
          if (Model.CurrentColumnId == col.Id)
              progressStopped = true;
} %>
</ul>