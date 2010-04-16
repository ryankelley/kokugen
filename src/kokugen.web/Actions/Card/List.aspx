<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Card.List" MasterPageFile="~/Shared/Site.Master"%>
<%@ Import Namespace="Kokugen.Web.Actions.Card"%>
<%@ Import Namespace="FubuMVC.Core.Urls"%>

<asp:Content ID="THISCONTENTAREAID" ContentPlaceHolderID="mainContent" runat="server">
    <%= this.CSS("board.css") %>
        <style type="text/css">
    
    h2
    {
    color:#333;	
    
    }
    
    table
        {
            border-collapse:collapse;
            width:1024px;
            padding:10px;
            border:5px solid gray;
            margin:10px;
        }
table, td, th
    {
        border:3px solid black;
        border-style:inset;
        text-align:center;
        background-color:#ddd;
        color:#555;
    }
th
    {
        background-color:#888;
        color:#222;
        text-shadow: 0 1px #bbb;
    }
</style>

<div align=center>
    <table> 
    <tr>
        <th>
            #
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
