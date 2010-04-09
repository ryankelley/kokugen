<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="Kokugen.Web.Actions.Users.Details.Details"  MasterPageFile="~/Shared/Site.Master" %>

<asp:Content ContentPlaceHolderID=mainContent runat=server>
<h2>User Details</h2>

<%=this.Show(x => x.User.UserName) %>
<%=this.Show(x => x.User.Email) %>



</asp:Content>