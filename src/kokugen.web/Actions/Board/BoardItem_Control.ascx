<%@ Control Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Board.BoardItem_Control"  %>


    <span class="project-name">
		<%= this.DisplayFor(x => x.Name) %>
	</span>
	
	<span class="project-name">
		<%= this.DisplayFor(x => x.Description) %>
	</span>


