<%@ Control Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Board.BoardPhase_Control"  %>
<%@ Import Namespace="Kokugen.Web.Actions.BoardColumn"%>
<div class="column">
<div class="board-phase-header"><%= Model.Name %> <%= Model.CardLimit > 0 ? "(" + Model.CardLimit + ")" : "" %></div>
<ul class="card-list ui-sortable" id="<%= Model.Id %>" limit="<%= Model.CardLimit %>"></ul>
</div>