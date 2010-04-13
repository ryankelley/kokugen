<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Users.List"
 MasterPageFile="~/Shared/Site.Master" %>
<%@ Import Namespace="Kokugen.Web.Actions.Users.Details"%>
<%@ Import Namespace="HtmlTags"%>
<%@ Import Namespace="Kokugen.Web.Actions.Users"%>
<%@ Import Namespace="System.Linq"%>

<asp:Content ContentPlaceHolderID="mainContent" runat="server">
    <h3>Users</h3>
    <div class="allUsers">
    <% if(Model.Users.Count > 0){ %>
		<ul class="users">
			<% foreach(var user in Model.Users){ %>
			<li>
				<span class="username"><a href="<%=Urls.UrlFor(new UserDetailsRequest(){Username = user.UserName}) %>"><%=user.UserName %></a></span>
				<span class="email"><a href="mailto:<% =user.Email%>"><% =user.Email %></a></span>
                
			</li>
			<% } %>
		</ul>
		<ul class="paging">

			<% if (Model.Users.IsFirstPage){ %>
			<li>First</li>
			<li>Previous</li>
			<% }else{ %>
			<li><%=new LinkTag("First",Model.FirstPageLink)%></li>
			<li><%=new LinkTag("Previous",Model.PrevLink)%></li>
			<% } %>

			<li>Page <% =Model.Users.PageNumber%> of <% =Model.Users.PageCount%></li>

			<% if (Model.Users.IsLastPage){ %>
			<li>Next</li>
			<li>Last</li>
			<% }else{ %>
			<li><%=new LinkTag("Next",Model.NextLink)%></li>
			<li><%=new LinkTag("Last",Model.LastPageLink)%></li>
			<% } %>
		</ul>
	<% }else{ %>
		<p>No users have registered.</p>
	<% } %>
	</div>
    
  

</asp:Content>

<asp:Content ContentPlaceHolderID=head runat=server>
<script type="text/javascript">
 
</script>
</asp:Content>
