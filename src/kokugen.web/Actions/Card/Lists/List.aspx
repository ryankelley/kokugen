<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Card.Lists.List" MasterPageFile="~/Shared/Project.Master"%>
<%@ Import Namespace="Kokugen.Web.Actions.Card.Lists"%>
<%@ Import Namespace="FubuMVC.Core.Urls"%>
<asp:Content ID="cardListHead" ContentPlaceHolderID="head" runat="server">

<style type="text/css">
 .content { 
      display: table;
    width: 100%;
  background-image: -moz-linear-gradient(top, #F0F0F0, #FFFFFF 20%); /* FF3.6 */
  background-image: -webkit-gradient(linear,left top,left bottom,color-stop(0, #d0d0d0),color-stop(1, #FFFFFF)); /* Saf4+, Chrome */
            filter:  progid:DXImageTransform.Microsoft.gradient(startColorStr='#d0d0d0', EndColorStr='#FFFFFF'); /* IE6,IE7 */
        -ms-filter: "progid:DXImageTransform.Microsoft.gradient(startColorStr='#d0d0d0', EndColorStr='#FFFFFF')"; /* IE8 */ 
}
ul.cards 
{
    display: table;
    width:100%;
}

li.header { text-align: center; font-weight: bold; background-color: #666; color:#FFF; }
ul li { display: table-row; }
ul li span { border-bottom:1px solid #999999;              
border-right:1px solid #999999;
display:table-cell;
padding:7px; }

ul.cards li.even { background-color: #DEDEDE; }
ul.cards li:hover { background-color:#91B5D8;
border-color:#233644; }

span.number { min-width: 30px; width: 40px; }
span.number:hover { text-indent:16px; background: transparent url('/content/images/magnify16.png') no-repeat 8px 10px;}
li.completed span { text-decoration: line-through; }
li.header span.number { text-indent: 0px; }
.number a { color: #333; text-decoration: none; }
.number a:hover  
{
    text-indent: 16px;}
    
.grey {
background-color:#DDDDDD;
border-color:#CCCCCC;
}
.blue {
background-color:#70A5D8;
border-color:#233644;
}
.yellow {
background-color:#ECD800;
border-color:#9C8C02;
}
.orange {
background-color:#E49703;
border-color:#B67902;
}
.teal {
background-color:#029DB6;
border-color:#027E92;
}
</style>
</asp:Content>
<asp:Content ID="cardListMain" ContentPlaceHolderID="mainContent" runat="server">
<ul class="cards">
<li class="header">
<span class="number">Number</span>
<span class="color">Color</span>
<span class="title">Title</span>
<span class="size">Size</span>
<span class="priority">Priority</span>
<span class="deadline">Deadline</span>
<span class="status">Status</span>
<span class="tasks">Tasks</span>
</li>

    <%=this.PartialForEach(x => x.Cards).Using<CardItem_Control>()  %>
</ul>

</asp:Content>
