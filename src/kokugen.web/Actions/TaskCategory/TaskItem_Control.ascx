<%@ Control Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.TaskCategory.TaskItem_Control"  %>
<%@ Import Namespace="Kokugen.Web.Actions.TaskCategory"%>


    <div class="col-title" text-align=left>
		<%= this.DisplayFor(x => x.Name) %>
		<%= this.LinkTo((new RemoveTaskInput(){Id = Model.Id})).Text("X") %>
	</div>
	
	


