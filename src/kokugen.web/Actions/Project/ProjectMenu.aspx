<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Project.ProjectMenu"%>
<%@ Import Namespace="Kokugen.Web.Actions.Project" %>
<ul class="dropdown">
    <%= this.PartialForEach(m => m.ProjectList).Using<ProjectMenu_Item>() %>
</ul>