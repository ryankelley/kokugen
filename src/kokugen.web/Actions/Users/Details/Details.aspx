<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="Kokugen.Web.Actions.Users.Details.Details"  MasterPageFile="~/Shared/Site.Master" %>

<asp:Content ContentPlaceHolderID=mainContent runat=server>
<div>

<h2>User Details</h2> <br />

 <h3> User Name : <%= this.DisplayFor( x => x.User.UserName) %></h3> 
 <h3> User Email : <%= this.DisplayFor( x => x.User.Email) %></h3> 
 
 </div>



</asp:Content>