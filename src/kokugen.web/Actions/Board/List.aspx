<%@ Page Language="C#" Inherits="Kokugen.Web.Actions.Board.List" AutoEventWireup="true" MasterPageFile="~/Shared/Site.Master" %>
<%@ Import Namespace="Kokugen.Web.Actions.Board"%>
<%@ Import Namespace="StructureMap.Query"%>
<%@ Import Namespace="Kokugen.Web.Conventions"%>
<%@ Import Namespace="Kokugen.Web.Actions.Company"%>
<%@ Import Namespace="Kokugen.Core"%>
<%@ Import Namespace="FubuMVC.Core.Urls"%>
<%@ Import Namespace="HtmlTags"%>
<asp:Content ID="CompanyListHead" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="THISCONTENTAREAID" ContentPlaceHolderID="mainContent" runat="server">

    <div class="content">
    <%= this.PartialForEach(m => m.BoardColumns).Using<BoardItem_Control>() %>
    </div>
   
   


</asp:Content>