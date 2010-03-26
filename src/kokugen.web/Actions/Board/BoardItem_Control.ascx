<%@ Control Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Board.BoardItem_Control"  %>


    <div class="col-title">
		<%= this.DisplayFor(x => x.Name) %>
	</div>
	
	<div class="col-desc">
		<%= this.DisplayFor(x => x.Description) %>
	</div>


