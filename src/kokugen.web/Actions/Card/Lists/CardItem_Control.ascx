<%@ Control Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Card.Lists.CardItem_Control"  %>
<%@ Import Namespace="StructureMap.Query"%>
<%@ Import Namespace="Kokugen.Web.Actions.Card" %>
<%@ Import Namespace="System.Linq" %>
<span class="number"><%=this.LinkTo(new CardDetailInputModel() { Id = Model.Id }).Text(Model.CardNumber.ToString()) %></span>
<span class="<%= Model.Color %>"></span>
<%= this.DisplayFor(x => x.Title).AddClass("title") %>
<%= this.DisplayFor(x => x.Size).AddClass("size") %>

<%= this.DisplayFor(x => x.Priority).AddClass("priority") %>
<span class="deadline"><%= Model.Deadline == null ? "" : Model.Deadline.Value.ToShortDateString()%></span>
<%= this.DisplayFor(x => x.Status).AddClass("status") %>
<span class="tasks"><%= Model.GetTasks.Count() %></span>
<span class="card-link"><%=this.LinkTo(new CardDetailInputModel() { Id = Model.Id }).NoClosingTag()
                           + "<img src=\"/content/images/magnify16.png\" alt=\"edit\" /></a>"%> </span>
   
		
	
	
		




