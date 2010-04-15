<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" Inherits="Kokugen.Web.Actions.Account.Manage.ManageAccount" %>
<%@ Import Namespace="Kokugen.Web.Actions.Account.Manage" %>
<%@ Import Namespace="Kokugen.Web.Actions.Account.Manage.ChangePassword" %>
<%@ Import Namespace="System.Linq" %>

<asp:Content ContentPlaceHolderID="mainContent" runat="server">

<%=this.FormFor(new ManageAccountModel(){Id = Model.Id}).Id("manage-form") %>
<fieldset>
<legend>Manage Account</legend>
<%=this.Edit(x => x.User.FirstName) %>
<%=this.Edit(x => x.User.LastName) %>
<%=this.Edit(x => x.User.Email) %>

<%=this.LinkTo(new ChangePasswordRequest(){Id = Model.Id}).Text("Change Password").Id("change-password").Title("Change Password") %>

</fieldset>

<input type="submit" value="Save" />
<%=this.EndForm() %>

</asp:Content>

<asp:Content ContentPlaceHolderID="head" runat="server">
        

    <script type="text/javascript">


        $(function () {
            $('#manage-form').submit(function () {
                ValidateAndSave(HandleAjaxResponse, $(this), false);
                return false;
            });


            $('#change-password').ajaxDialog({
                onComplete: HandleAjaxResponse,
                dataType: 'json'
            });

        });
    
    </script>
</asp:Content>
