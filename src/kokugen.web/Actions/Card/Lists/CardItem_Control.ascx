<%@ Control Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Card.Lists.CardItem_Control"  %>
<%@ Import Namespace="StructureMap.Query"%>
<%@ Import Namespace="Kokugen.Web.Actions.Card" %>

    <tr>
    
        <td>
            <%= this.DisplayFor(x => x.CardNumber) %>
        </td>
        
        <td class="<%=Model.Color %>" >
<%--            <%= this.DisplayFor(x => x.Color) %>--%>
        </td>
        <td>
            <%= this.DisplayFor(x => x.Details) %>
        </td> 
        <td>
         <%= this.DisplayFor(x => x.Title) %>
         </td>
        <td>
         <%= this.DisplayFor(x => x.Size) %>
         </td>
         <td>
            <%= this.DisplayFor(x => x.Deadline.Value.Month) + "/" + this.DisplayFor(x => x.Deadline.Value.Day) + "/" + this.DisplayFor(x => x.Deadline.Value.Year)%>
        </td>
        <td>
        <%=this.LinkTo(new CardDetailInputModel() { Id = Model.Id }).NoClosingTag() + "<img src=\"/content/images/magnify16.png\" alt=\"edit\" /></a>"%>
        </td>
         



    </tr>

   
		
	
	
		




