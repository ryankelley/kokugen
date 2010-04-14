<%@ Page Language="C#" AutoEventWireup="true"  Inherits="Kokugen.Web.Actions.Account.Password.PasswordRecoveryForm" %>
<%@ Import Namespace="Kokugen.Web.Actions.Account.Password" %>



<%if(Model.Settings.RequiresQuestionAndAnswer) {%>

<%=this.FormFor<RecoverPasswordModel>() %>
    <%=this.Edit(x=>x.Question) %>
    <%=this.Edit(x=>x.Answer) %>
<%=this.EndForm() %>

<%}else{ %>

<script type="text/javascript">
    $(function () {
        recoverPassword('<%=Urls.UrlFor(new RecoverPasswordModel()) %>', {}, function (data) { alert(data.Item); });
    });

</script>

<%} %>

<script type="text/javascript">

    function recoverPassword(url, data, cb) {
        $.post(url, data, cb, 'json');
    };

</script>