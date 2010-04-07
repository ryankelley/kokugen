<%@ Control Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.TimeRecord.TimeRecord_Control"  %>
<%@ Import Namespace="Kokugen.Web.Actions.TimeRecord"%>



    <tr>
        <td>
            User Name
        </td>
        <td>
            <%= this.DisplayFor(x => x.Description) %>
        </td>
        <td>
            <%= this.DisplayFor(x => x.StartTime) %>
        </td>
        <td>
            <%= this.DisplayFor(x => x.EndTime) %>
        </td>
        <td>
            <%= this.DisplayFor(x => x.Duration) %>
        </td>
        <td>
            <%= this.DisplayFor(x => x.Duration) %>
        </td>
        <td>
            <%= this.DisplayFor(x => x.Task.Name) %>
        </td>
        <td> 
            <%= this.DisplayFor(x => x.Project.Name) %>
        </td>              
	</tr>	

	
	
	


