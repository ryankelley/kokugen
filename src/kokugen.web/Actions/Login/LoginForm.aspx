<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Login.LoginForm"  
MasterPageFile="~/Shared/Site.Master" %>
<%@ Import Namespace="Kokugen.Web.Actions.Login"%>

 
<asp:Content ContentPlaceHolderID="mainContent" runat="server">

<%= this.FormFor(new LoginModel()) %>
    <%= this.Edit(x => x.Login) %>
    <input type='submit' value='login' />
<%= this.EndForm() %>
</asp:Content>