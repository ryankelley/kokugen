<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Account.Manage.ChangePassword.ChangePassword" %>
<%@ Import Namespace="Kokugen.Web.Actions.Account.Manage.ChangePassword" %>


<%=this.FormFor<ChangePasswordModel>() %>
<fieldset>
    <legend>Change Password</legend>
    <%=this.InputFor(x => x.Id).Hide() %>
    <%=this.Edit(x => x.OldPassword) %>
    <%=this.Edit(x => x.NewPassword) %>
    <%=this.Edit(x => x.ConfirmPassword) %>

</fieldset>
<%=this.EndForm() %>

