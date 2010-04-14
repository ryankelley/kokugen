<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" Inherits="Kokugen.Web.Actions.Account.Manage.ManageAccount" %>
<%@ Import Namespace="Kokugen.Web.Actions.Account.Manage" %>

<asp:Content ContentPlaceHolderID="mainContent" runat="server">

<%=this.FormFor(new ManageAccountModel(){Id = Model.Id}) %>
<fieldset>
<legend>Manage Account</legend>
<%=this.Edit(x => x.User.FirstName) %>
<%=this.Edit(x => x.User.LastName) %>
<%=this.Edit(x => x.User.Email) %>

<a href="#">Change password</a>
</fieldset>

<input type="submit" value="Save" />
<%=this.EndForm() %>

</asp:Content>
