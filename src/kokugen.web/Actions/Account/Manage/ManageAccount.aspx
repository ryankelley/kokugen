<%@ Page Title="" Language="C#" 

Inherits="Kokugen.Web.Actions.Account.Manage.ManageAccount" %>
<%@ Import Namespace="Kokugen.Web.Actions.Account.Manage" %>
<%@ Import Namespace="Kokugen.Web.Actions.Account.Manage.ChangePassword" %>
<%@ Import Namespace="System.Linq" %>



<%=this.FormFor(new ManageAccountModel(){Id = Model.Id}) %>

<%=this.Edit(x => x.User.FirstName) %>
<%=this.Edit(x => x.User.LastName) %>
<%=this.Edit(x => x.User.Email) %>

<%=this.LinkTo(new ChangePasswordRequest(){Id = Model.Id}).Text("Change Password").Id("change-password").Title("Change Password") %>



<%=this.EndForm() %>

      <script type="text/javascript">
          $(function () {
              $('#change-password').ajaxDialog({ onComplete: HandleAjaxResponse, dataType: 'json' });
          });

      </script>


