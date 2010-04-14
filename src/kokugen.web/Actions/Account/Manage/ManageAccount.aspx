<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" Inherits="Kokugen.Web.Actions.Account.Manage.ManageAccount" %>
<%@ Import Namespace="Kokugen.Web.Actions.Account.Manage" %>
<%@ Import Namespace="Kokugen.Web.Actions.Account.Manage.ChangePassword" %>

<asp:Content ContentPlaceHolderID="mainContent" runat="server">

<%=this.FormFor(new ManageAccountModel(){Id = Model.Id}) %>
<fieldset>
<legend>Manage Account</legend>
<%=this.Edit(x => x.User.FirstName) %>
<%=this.Edit(x => x.User.LastName) %>
<%=this.Edit(x => x.User.Email) %>

<%=this.LinkTo(new ChangePasswordRequest(){Id = Model.Id}).Text("Change Password").Id("change-password") %>

</fieldset>

<input type="submit" value="Save" />
<%=this.EndForm() %>

</asp:Content>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#change-password').ajaxDialog({
                onComplete: function () {

                },
                dataType: 'json'
            });

        });
    
    </script>
</asp:Content>
