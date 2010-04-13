<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Card.Add" MasterPageFile="~/Shared/Site.Master" %>
<%@ Import Namespace="Kokugen.Web.Actions.Card"%>
<div id="project-form-container" class="hide">
<%= this.FormFor(new CardInputFormModel()).Id("project-form")%>
    <%= this.Edit(x => x.Card.Title) %>
    <%= this.Edit(x => x.Card.Details) %>
    <%= this.Edit(x => x.Card.Project) %>
    <input type="submit" value="Submit" />
<%= this.EndForm() %>
   
</div>