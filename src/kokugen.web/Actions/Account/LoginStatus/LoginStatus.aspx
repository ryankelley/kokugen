<%@ Page Language="C#" Inherits="Kokugen.Web.Actions.Account.LoginStatus.LoginStatus" %>

<%@ Import Namespace="Kokugen.Web.Actions.Account.Register" %>
<%@ Import Namespace="Kokugen.Web.Actions.Account.Manage" %>
<%if (Model.IsAuthenticated)
  { %>
<div class="gravatar">
    <img src="http://www.gravatar.com/avatar/<%= Model.EmailAddress.ToGravatarHash() %>?d=monsterid&s=60"
        alt="gravatar" /></div>
<div class="user-actions">
    Welcome <b>
        <%=this.LinkTo(new ManageAccountRequest(){UserName = Model.UserName}).Text(Model.UserName).Title("Manage Account").Id("manage-account") %></b>!
    [ <a href="<%=Model.RawUrl %>">Log Off</a> ]
</div>
      <script type="text/javascript">
          $(function () {
              $('#manage-account').ajaxDialog({ onComplete: HandleAjaxResponse, dataType: 'json' });
          });

      </script>

<%}
  else
  {%>
<div class="user-actions">
    [ <a href="<%=Model.RawUrl %>">Log In</a> ] <span>
        <%=this.LinkTo(new RegisterAccountRequest()).Text("Register") %></span>
</div>
<%} %>
