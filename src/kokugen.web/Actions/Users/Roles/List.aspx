<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Users.Roles.List" %>


<ul>
<%
    foreach (var role in Model.Roles)
    {%>
        <li><%=role %></li>
    <%} %>
</ul>