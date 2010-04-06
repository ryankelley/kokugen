<%@ Control Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Board.BoardPhase_Control"  %>
<%@ Import Namespace="Kokugen.Web.Actions.BoardColumn"%>
<div class="column">
<div class="board-phase-header"><%= Model.Name %> <%= Model.Limit > 0 ? "(" + Model.Limit + ")" : "" %></div>
<ul class="card-list ui-sortable" id="<%= Model.Id %>" ></ul>
</div>