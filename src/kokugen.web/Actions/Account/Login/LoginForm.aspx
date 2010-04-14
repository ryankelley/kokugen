<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Account.Login.LoginForm"  
MasterPageFile="~/Shared/Site.Master" %>
<%@ Import Namespace="Kokugen.Web.Actions.Account.Login" %>
<%@ Import Namespace="FubuCore" %>
<%@ Import Namespace="Kokugen.Web.Actions.Account.Password" %>

 
<asp:Content ContentPlaceHolderID="mainContent" runat="server">
<%if (Model.Message.IsNotEmpty())
  {%>
        <h3 style="color:Red;"><%=Model.Message %></h3>
<%
  }%>
<%= this.FormFor(new LoginModel(){ReturnUrl = Model.ReturnUrl}) %>
    <%= this.Edit(x => x.Login) %>
    <%=this.Edit(x => x.Password) %>
    <%=this.Edit(x => x.RememberMe) %>

    <input type='submit' value='login' />
<%= this.EndForm() %>

<%this.Partial<ResetPasswordPartialModel>(); %>

</asp:Content>