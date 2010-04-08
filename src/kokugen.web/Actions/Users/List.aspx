<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Users.List"
 MasterPageFile="~/Shared/Site.Master" %>
<%@ Import Namespace="System.Linq"%>

<asp:Content ContentPlaceHolderID="mainContent" runat="server">
    <%if (Model.Users.Any())
      { %>
      <ul>
                <%
          foreach (var user in Model.Users)
          {%>
              <li><span><%=user.UserName %></span></li>
              
         <% } %>
      </ul>   
    <%} %>

</asp:Content>

<asp:Content ContentPlaceHolderID=head runat=server>
<script type="text/javascript">
    $(function() {
         
     });
</script>
</asp:Content>
