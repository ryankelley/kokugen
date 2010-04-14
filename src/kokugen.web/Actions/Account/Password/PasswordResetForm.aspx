<%@ Page Language="C#" AutoEventWireup="true"  Inherits="Kokugen.Web.Actions.Account.Password.PasswordResetForm" %>
<%@ Import Namespace="Kokugen.Web.Actions.Account.Password" %>



<%if(Model.Settings.RequiresQuestionAndAnswer) {%>

<%=this.FormFor<ResetPasswordModel>() %>
    <%=this.InputFor(x=> x.Email).Id("email").Hide() %>
    <%=this.Edit(x=>x.Question) %>
    <%=this.Edit(x=>x.Answer) %>
    <input type="submit" value="Reset" />
<%=this.EndForm() %>

<%}else{ %>

<script type="text/javascript">
    $(function () {
        recoverPassword('<%=Urls.UrlFor(new ResetPasswordModel()) %>', {Email:<%=Model.Email %>}, function (data) { alert(data.Item); });
    });

</script>

<%} %>

<script type="text/javascript">

    function recoverPassword(url, data, cb) {
        $.post(url, data, cb, 'json');
    };

</script>