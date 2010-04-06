<%@ Control Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.TimeRecord.TimeRecord_Control"  %>
<%@ Import Namespace="Kokugen.Web.Actions.TimeRecord"%>


    <div class="col-title" text-align=left>
    <ul>
        <li>
            <%= this.DisplayFor(x => x.Description) %>
        
            <%= this.DisplayFor(x => x.Task.Name) %>
        
            <%= this.DisplayFor(x => x.Duration) %>
        
            <%= this.LinkTo(new RemoveTimeRecordInput()
                                        {
                                            Id = Model.Id
                                        }
                                                     ).Text("Delete") %>
        </li>
              
	</ul>	
	</div>
	
	
	


