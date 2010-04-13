<%@ Control Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.TaskCategory.TaskItem_Control"  %>
<%@ Import Namespace="Kokugen.Web.Actions.TaskCategory"%>
<%@ Import Namespace="HtmlTags" %>


    <tr>
        <td>
            <%= this.DisplayFor(x => x.Name) %>
        </td>
        <td>
            <%= new LinkTag("", "#").AddClass("delete-button").Attr("data", Model.Id).NoClosingTag() + "<img src=\" /content/images/delete.png\" alt=\"delete\" /></a>"%>
        </td>

    </tr>
		
		
	
	
	


