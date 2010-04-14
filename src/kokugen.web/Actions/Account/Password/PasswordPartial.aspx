<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Account.Password.PasswordPartial" %>
<%@ Import Namespace="Kokugen.Web.Actions.Account.Password" %>

<div id="password-recover-wrapper">
<% if (Model.Settings.EnablePasswordReset)
   {%>
    <%=this.LinkTo(new ResetPasswordRequest()).Id("forgot-password-link").Text("Forgot your password?")%>

    <div id="reset-wrapper" style="display:none;">
         <%=this.Edit(x => x.Email) %>
         <input type="button" value="Reset Password" id="reset" />
    </div>

    <script type="text/javascript">
        $(function () {
            $('#forgot-password-link').click(function (e) {
                e.preventDefault();
                $('#reset-wrapper').toggle();
            });

            $('#reset').click(function (e) {
                e.preventDefault();
                $('#password-reset-placeholder').load($('#forgot-password-link').attr('href') + $('#email').val());
            });

        });

    </script>

    <%
   }%>


</div>

<div id="password-reset-placeholder"></div>

