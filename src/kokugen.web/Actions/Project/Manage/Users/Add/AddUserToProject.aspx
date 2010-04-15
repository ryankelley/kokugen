<%@ Page Language="C#" AutoEventWireup="true"  Inherits="Kokugen.Web.Actions.Project.Manage.Users.Add.AddUserToProject" %>
<%@ Import Namespace="Kokugen.Web.Actions.Project.Manage.Users.Add" %>

<div>
    <%=this.FormFor<AddUserToProjectModel>() %>
        <%=this.InputFor(x => x.ProjectId).Hide() %>
        <%=this.Edit(x=> x.User) %>
        <%=this.Edit(x=> x.Role) %>
    <%=this.EndForm() %>
</div>