<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeleteProjectUser.aspx.cs" Inherits="Kokugen.Web.Actions.Project.Manage.Users.Delete.DeleteProjectUser" %>
<%@ Import Namespace="Kokugen.Web.Actions.Project.Manage.Users.Delete" %>
<%@ Import Namespace="System.Linq" %>

<div>
    <%=this.FormFor<DeleteProjectUserModel>().Attr("method","DELETE") %>
        <%=this.InputFor(x => x.ProjectId).Hide() %>
        <%=this.DropDownFor(x => x.User,()=>Model.Users.Select(x=> new ValueObject(x.Id.ToString(),x.DisplayName()))) %>
    <%=this.EndForm() %>
</div>
