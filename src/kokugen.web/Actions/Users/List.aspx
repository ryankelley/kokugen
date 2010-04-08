<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Users.List"
 MasterPageFile="~/Shared/Site.Master" %>
<%@ Import Namespace="Kokugen.Web.Actions.Users.Roles"%>
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
    
    <div id="roles-placeholder">
    
    </div>

</asp:Content>

<asp:Content ContentPlaceHolderID=head runat=server>
<script type="text/javascript">
    $(function() {
        $('#roles-placeholder').load('<%=Urls.UrlFor(new RolesListModel()) %>');
    });
</script>
</asp:Content>
