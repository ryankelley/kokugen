<%@ Control Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.TimeRecord.TimeRecord_Control"  %>
<%@ Import Namespace="Kokugen.Web.Actions.TimeRecord"%>


    <div class="col-title" text-align=left>
    <tr>
        <td>
            <%= this.DisplayFor(x => x.Description) %>
        </td>
        <td>
            <%= this.DisplayFor(x => x.Task.Name) %>
        </td>
        <td>
            <%= this.DisplayFor(x => x.Duration) %>
        </td>
        <td>
             <%= this.LinkTo(new RemoveTimeRecordInput()
                                        {
                                            Id=Model.Id
                                        }
                                                     ).Text("Delete") %>
        </td>
              
	</tr>	
	</div>
	
	
	


