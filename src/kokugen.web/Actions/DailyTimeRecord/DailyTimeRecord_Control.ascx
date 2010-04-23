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
            <span class="end-time" > <%if (Model.EndTime == null)
                                           Response.Write(new LinkTag("", "#").AddClass("stop-button").Attr("data", Model.Id).NoClosingTag().AddClass("icon") + "<img src=\" /content/images/stopsign.png\" alt=\"stop\" /></a>");
                                       else
                                           Response.Write(Model.EndTime);%></span></td>
        <td class="duration">
            <%= this.DisplayFor(x => x.Duration) + " " %>hrs
        </td>
                
	</tr>	

	
	
	


