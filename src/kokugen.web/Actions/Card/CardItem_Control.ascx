<%@ Control Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Card.CardItem_Control"  %>
<%@ Import Namespace="StructureMap.Query"%>

    <tr>
    
        <td>
            <%= this.DisplayFor(x => x.CardNumber) %>
        </td>
        <td>
            <%= this.DisplayFor(x => x.AssignedTo.UserName) %>
        </td> 
        <td class="<%=Model.Color %>">
<%--            <%= this.DisplayFor(x => x.Color) %>--%>
        </td>
        <td>
            <%= this.DisplayFor(x => x.Details) %>
        </td>
         <td>
            <%= this.DisplayFor(x => x.Deadline) %>
        </td>
         <td>
            <%= this.DisplayFor(x => x.DateCompleted) %>
        </td>                   



    </tr>

   
		
	
	
		




