<%@ Page Language="C#" AutoEventWireup="true"  Inherits="Kokugen.Web.Actions.Project.Manage.Users.Add.AddUserToProject" %>
<%@ Import Namespace="Kokugen.Web.Actions.Project.Manage.Users.Add" %>
<%@ Import Namespace="System.Linq" %>

<div>
    <%=this.FormFor<AddUserToProjectModel>() %>
        <%=this.InputFor(x => x.ProjectId).Hide() %>
        <%=this.DropDownFor(x=> x.User,()=>Model.Users.Select(x => new ValueObject(x.Id.ToString(),x.DisplayName()))) %>
        <%=this.Edit(x=> x.Role) %>
    <%=this.EndForm() %>
</div>