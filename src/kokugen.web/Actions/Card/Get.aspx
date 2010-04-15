<%@ Page Language="C#" Inherits="Kokugen.Web.Actions.Card.Get" AutoEventWireup="true" MasterPageFile="~/Shared/Project.Master" %>
<%@ Import Namespace="Kokugen.Web.Actions.Card" %>
<asp:Content ID="CardGetHead" ContentPlaceHolderID="head" runat="server">
<%= this.Script("board.js") %>
<%= this.Script("jquery.metadata.js")%>
<%= this.Script("jquery.jeditable.js")%>
<%= this.Script("dovetail.editing.js")%>
<script type="text/javascript">
    $(document).ready(function () {
        $('#tabs').tabs();
        dovetailEditableValues = <%= Model.ToJson() %>
        $('.editable').makeEditable('<%= Model.Id %>', "/Card/update");

    });
</script>
</asp:Content>
<asp:Content ID="CardGetMain" ContentPlaceHolderID="mainContent" runat="server">
<% this.Partial(new CompactCardFormInput{ Id = Model.ProjectId}); %>
<div class="contentWrapper">
<div class="main-panel">
        <div class="action-bar">
        <ul class="btns">
		<li id="ready"><a href="#">Ready</a></li>
		<li id="block"><a href="#">Block</a></li>
		<li id="color"><a href="#">Color</a></li>
		<li id="claim"><a href="#">Claim</a></li>
		<li id="delete"><a href="#">Delete</a></li>
	</ul></div>
        <div class="card-title"><%= this.EditInPlace(m => m.Title) %></div>
        <div class="card-process">This could Show different phases that the card has completed</div>
        <div id="tabs">
            <ul>
                <li><a href="#details"><span>Details</span></a></li>
            </ul>
            <div id="details" class="card-detail"><%= this.EditInPlace(m => m.Details)%></div>
        </div>
        
</div>
</div>
<div class="right-panel">
        <ul>
            <li class="card-number <%= Model.Color %>"><%= this.DisplayFor(m => m.CardNumber) %></li>
            <li class="hidden">Ready and blocked state can go here</li>
            <li class=""><div class="sidebar-title">Size</div><%= this.EditInPlace(m => m.Size)%></li>
            <li class=""><div class="sidebar-title">Priority</div><%= this.EditInPlace(m => m.Priority)%></li>
            <li class=""><div class="sidebar-title">Deadline</div><%= this.EditInPlace(m => m.Deadline) %></li>
            <li class=""><div class="sidebar-title">Owner</div></li>
            <li class=""><div class="sidebar-title">Created</div></li>
            <li class=""><div class="sidebar-title">Started</div></li>
            <li class=""><div class="sidebar-title">Finished</div></li>
            
            <li class=""></li>
        </ul>
</div>

</asp:Content>