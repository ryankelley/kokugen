<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Card.List" MasterPageFile="~/Shared/Site.Master"%>
<%@ Import Namespace="Kokugen.Web.Actions.Card"%>
<%@ Import Namespace="FubuMVC.Core.Urls"%>

<asp:Content ID="THISCONTENTAREAID" ContentPlaceHolderID="mainContent" runat="server">
    <%= this.CSS("board.css") %>
    <style type="text/css">
    table
        {
            border-collapse:collapse;
            width:640px;
            padding:10px;
            border:5px solid gray;
            margin:10px;
        }
table, td, th
    {
        border:3px solid black;
        border-style:inset;
        text-align:center;
        background-color:#CEBEB4;
        color:#49657D;
    }
th
    {
        background-color:#9e9993;
        color:black;
    }
</style>
<div align=center>
    <table> 
    <tr>
        <th>
            Card Number
        </th>
        <th>
            Assigned 
        </th>
        <th>
            Color
        </th>
        <th>
            Details
        </th>
        <th>
            Deadline
        </th>
        <th>
            Completed
        </th>
    </tr>
        <%=this.PartialForEach(x => x.Cards).Using<CardItem_Control>() %>
    </table>    
 </div>
</asp:Content>
