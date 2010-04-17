<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Project.MenuList.ProjectMenu"%>
<%@ Import Namespace="Kokugen.Web.Actions.Project" %>
<%@ Import Namespace="Kokugen.Web.Actions.Project.MenuList" %>
<ul class="dropdown">
    <%= this.PartialForEach(m => m.ProjectList).Using<ProjectMenu_Item>() %>
</ul>