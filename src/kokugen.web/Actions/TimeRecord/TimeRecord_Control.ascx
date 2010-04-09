<%@ Control Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.TimeRecord.TimeRecord_Control"  %>
<%@ Import Namespace="HtmlTags"%>
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
        <td class="end-time">
        
            <%if (Model.EndTime == null)
                    Response.Write(new LinkTag("", "#").AddClass("stop-button").Attr("data", Model.Id).NoClosingTag().AddClass("icon") + "<img src=\" /content/images/stopsign.png\" alt=\"view board\" /></a>");
                else
                    Response.Write(this.DisplayFor(x => x.EndTime));%>
        </td>
        <td class="duration">
            <%= this.DisplayFor(x => x.Duration) + " " %>hrs
        </td>
        <td class="billable">
            <%= this.DisplayFor(x => x.Billable) + " " %>hrs
        </td>
        <td>
            <%= this.DisplayFor(x => x.Task.Name) %>
        </td>
        <td> 
            <a href="/project/<%= Model.ProjectId %>"><%=Model.ProjectName %></a>
        </td>              
	</tr>	

	
	
	


