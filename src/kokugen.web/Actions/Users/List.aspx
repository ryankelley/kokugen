<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Users.List"
 MasterPageFile="~/Shared/Site.Master" %>
<%@ Import Namespace="System.Linq"%>

<asp:Content ContentPlaceHolderID="mainContent" runat="server">
    <%if (Model.Users.Any())
      { %>
                <%
          foreach (var user in Model.Users)
          {%>
              
              
         <% } %>
                
    <%} %>

</asp:Content>
