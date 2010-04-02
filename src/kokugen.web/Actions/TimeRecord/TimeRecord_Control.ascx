<%@ Control Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.TimeRecord.TimeRecord_Control"  %>


    <div class="col-title" text-align=left>
        <td>
            <%= this.DisplayFor(x => x.Description) %>
        </td>
        <td>
            <%= this.DisplayFor(x => x.Task.Name) %>
        </td>
        <td>
            <%= this.DisplayFor(x => x.Duration) %>
        </td>       
		
	</div>
	
	
	


