<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActivateAccount.aspx.cs" 
Inherits="Kokugen.Web.Actions.Account.Activate.ActivateAccount"
 MasterPageFile="~/Shared/Site.Master" %>
<%@ Import Namespace="Kokugen.Web.Actions.Account.Login" %>

 <asp:Content ContentPlaceHolderID=mainContent runat=server >
    <h3>Your account is now activated!</h3>

    <p>
        Thank you for registering! You may now <%=this.LinkTo(new LoginFormModel()).Text("login") %>.
    </p>

 </asp:Content>