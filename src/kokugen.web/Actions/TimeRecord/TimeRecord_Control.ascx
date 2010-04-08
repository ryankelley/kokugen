<%@ Control Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.TimeRecord.TimeRecord_Control"  %>
<%@ Import Namespace="Kokugen.Web.Actions.TimeRecord"%>



    <tr>
        <td>
            <%= this.DisplayFor(x => x.User.UserName) +" "+  this.DisplayFor(x => x.User.UserName) %>
        </td>
        <td>
            <%= this.DisplayFor(x => x.Description) %>
        </td>
        <td>
            <%= this.DisplayFor(x => x.StartTime) %>
        </td>
        <td>
<%--        <% this.Partial(new StopTimeRecordFormModel(){TimeRecord = Model}); %>--%>
            <%if (Model.EndTime == null)
                    Response.Write(this.LinkTo(new StopTimeRecordModel(){Id =Model.Id}).NoClosingTag().AddClass("icon") + "<img src=\" /content/images/stopsign.png\" alt=\"view board\" /></a>");
                else
                    Response.Write(this.DisplayFor(x => x.EndTime));%>
        </td>
        <td>
            <%= this.DisplayFor(x => x.Duration) %>
        </td>
        <td>
            <%= this.DisplayFor(x => x.Billable) %>
        </td>
        <td>
            <%= this.DisplayFor(x => x.Task.Name) %>
        </td>
        <td> 
            <%= this.DisplayFor(x => x.Project.Name) %>
        </td>              
	</tr>	

	
	
	


