<%@ Control Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Company.CompanyItem_Control"  %>
<%@ Import Namespace="HtmlTags" %>


    <tr>
        <td>
            <%= this.DisplayFor(x => x.Name) %>
        </td>
        <td>
            <%= new LinkTag("", "#").AddClass("edit-button").Attr("data", Model.Id).NoClosingTag() + "<img src=\"/content/images/edit2.png\" alt=\"edit\" /></a>" 
                                + " | " +  new LinkTag("", "#").AddClass("delete-button").Attr("data", Model.Id).NoClosingTag() + "<img src=\" /content/images/delete.png\" alt=\"delete\" /></a>"%>
        </td>

    </tr>
		
		
	
	
	


