<%@ Control Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Board.BoardItem_Control"  %>
<%@ Import Namespace="Kokugen.Web.Actions.BoardColumn"%>


    <div class="col-title">
		<%= this.DisplayFor(x => x.Name) %>
	</div>
	
	<div class="col-desc">
		<%= this.DisplayFor(x => x.Description) %>
	
	    <div class="col-links hidden">
	    <img src="/content/images/card_edit.png" alt="Edit Column" />
	    <a href="#" data="<%= Model.Id %>" class="removeLink"><img src="/content/images/card_delete.png" alt="Delete Column" /></a>
	    </div>
	</div>


