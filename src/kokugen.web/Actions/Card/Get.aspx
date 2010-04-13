<%@ Page Language="C#" Inherits="Kokugen.Web.Actions.Card.Get" AutoEventWireup="true" MasterPageFile="~/Shared/Project.Master" %>
<%@ Import Namespace="Kokugen.Web.Actions.Card" %>
<asp:Content ID="CardGetHead" ContentPlaceHolderID="head" runat="server">
<%= this.Script("board.js") %>
<script type="text/javascript">

</script>
</asp:Content>
<asp:Content ID="CardGetMain" ContentPlaceHolderID="mainContent" runat="server">
<% this.Partial(new CompactCardFormInput{ Id = Model.Card.ProjectId}); %>
<div class="card-info">
    <div class="main-panel">
        <div class="action-bar"></div>
        <div class="card-title"><%= this.DisplayFor(m => m.Card.Title) %></div>
        <div class="card-process"></div>
        <div class="card-detail"><%= this.DisplayFor(m => m.Card.Details) %></div>
    </div>
    <div class="right-panel">
        <ul>
            <li class="card-number"><%= this.DisplayFor(m => m.Card.CardNumber) %></li>
            <li class=""></li>
            <li class=""><%= this.DisplayFor(m => m.Card.Size) %></li>
            <li class=""><%= this.DisplayFor(m => m.Card.Priority) %></li>
            <li class=""><%= this.DisplayFor(m => m.Card.Deadline) %></li>
            
            <li class=""></li>
        </ul>
    </div>
</div>
</asp:Content>