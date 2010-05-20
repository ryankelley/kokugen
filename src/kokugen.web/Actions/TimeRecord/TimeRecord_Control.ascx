<%@ Control Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.TimeRecord.TimeRecord_Control"  %>
<%@ Import Namespace="HtmlTags"%>

 
        <% 
            if (Model.User != null)
     Response.Write(this.DisplayFor(x => x.User.UserName));
     else
            {
                Response.Write("<span></span>");
            }
             %>               
            <span class="description" ><%= Model.Description %></span>                   
            <%= this.DisplayFor(x => x.StartTime) %>           
            <span class="end-time" > <%if (Model.EndTime == null)
                    Response.Write(new LinkTag("", "#").AddClass("stop-button").Attr("data", Model.Id).NoClosingTag().AddClass("icon") + "<img src=\" /content/images/stopsign.png\" alt=\"stop\" /></a>");
                else
                    Response.Write(Model.EndTime);%></span>            
            <span class="duration"><%= Model.Duration + " " %>hrs</span>            
            <span class="billable"><%= Model.Billable + " " %>hrs</span>            
            <%= this.DisplayFor(x => x.Task.Name) %>          
            <span><a href="/project/<%= Model.Project.Id %>"><%=Model.Project.Name %></a></span>
