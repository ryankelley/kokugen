<%@ Control Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Project.ProjectItem_Control"  %>
<a title="Click to view this project" href="/project/<%= Model.Id %>">
    <span class="project-name">
		<%= this.DisplayFor(x => x.Name) %>
	</span>
	<span class="project-stats">
		Some Status Here
	</span>
	<div class="project-owner">
		<%= this.DisplayFor(x => x.Company.Name) %>

	</div>
</a>


