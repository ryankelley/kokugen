<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Card.List" MasterPageFile="~/Shared/Site.Master"%>
<%@ Import Namespace="Kokugen.Web.Actions.Card"%>

<asp:Content ID="CompanyListHead" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="THISCONTENTAREAID" ContentPlaceHolderID="mainContent" runat="server">
    <p>Hello, world!</p>
    <% this.Partial(new CardInputFormModel()); %>
</asp:Content>
