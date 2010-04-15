using System;
using FubuMVC.Core.View;
using Kokugen.Core.Attributes;
using Kokugen.Web.Conventions;

namespace Kokugen.Web.Actions.Project.Manage.Users.Add
{
    public class AddUserToProjectAction
    {
        public AddUserToProjectModel Query(AddUserToProjectRequest request)
        {
            return new AddUserToProjectModel(){ProjectId = request.Id};
        }

        public AjaxResponse Command(AddUserToProjectModel model)
        {
            return new AjaxResponse(){Success = true, Item = "User has been add to the project"};
        }
    }

    public class AddUserToProjectRequest:IRequestById
    {
        public Guid Id { get; set;}
    }

    public class AddUserToProjectModel
    {
        public Guid ProjectId { get; set;}
        [ValueOf("User")]
        public Guid User { get; set; }
        [ValueOf("Role")]
        public Guid Role { get; set; }
    }

    public class AddUserToProject : FubuPage<AddUserToProjectModel>{}
}