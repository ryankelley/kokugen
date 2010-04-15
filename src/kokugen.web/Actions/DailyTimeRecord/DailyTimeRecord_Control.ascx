<%@ Control Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.DailyTimeRecord.DailyTimeRecord_Control"  %>
<%@ Import Namespace="HtmlTags"%>



    <tr>
        <td>
        <% 
            if (Model.User != null)
     Response.Write(this.DisplayFor(x => x.User.UserName) );
             %>
        </td>
        <td>
            <%= this.DisplayFor(x => x.StartTime) %>
        </td>
        <td class="end-time">
        
            <%Response.Write(this.DisplayFor(x => x.EndTime));%>
        </td>
        <td class="duration">
            <%= this.DisplayFor(x => x.Duration) + " " %>hrs
        </td>
                
	</tr>	

	
	
	


