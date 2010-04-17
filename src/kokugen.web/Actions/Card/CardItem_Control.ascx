<%@ Control Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Card.CardItem_Control"  %>
<%@ Import Namespace="StructureMap.Query"%>

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
            <%= this.DisplayFor(x => x.Deadline.Value.Month) + "/" + this.DisplayFor(x => x.Deadline.Value.Day) + "/" + this.DisplayFor(x => x.Deadline.Value.Year)%>
        </td>
         



    </tr>

   
		
	
	
		




