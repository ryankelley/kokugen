<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Account.Password.PasswordPartial" %>
<%@ Import Namespace="Kokugen.Web.Actions.Account.Password" %>

<div id="password-recover-wrapper">
<% if (Model.Settings.EnablePasswordReset || Model.Settings.EnablePasswordRetrieval)
   {%>
    <%=this.LinkTo(new RecoverPasswordRequest()).Id("forgot-password-link").Text("Forgot your password?")%>

    <div id="recover-wrapper" style="display:none;">
         <%=this.Edit(x => x.Email) %>
         <input type="button" value="Recover Password" id="recover" />
    </div>

    <script type="text/javascript">
        $(function () {
            $('#forgot-password-link').click(function (e) {
                e.preventDefault();
                $('#recover-wrapper').toggle();
            });

            $('#recover').click(function (e) {
                e.preventDefault();
                $('#password-recover-placeholder').load($('#forgot-password-link').attr('href')+$('#email').val());
            });

        });

    </script>

    <%
   }%>


</div>

<div id="password-recover-placeholder"></div>

