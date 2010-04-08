<%@ Control Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.TimeRecord.TimeRecord_Control"  %>
<%@ Import Namespace="Kokugen.Web.Actions.TimeRecord"%>



    <tr>
        <td>
            <%= this.DisplayFor(x => x.User.FirstName) +" "+  this.DisplayFor(x => x.User.LastName) %>
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
            <a href="/project/<%= Model.Project.Id %>"><%=Model.Project.Name %></a>
        </td>              
	</tr>	

	
	
	


