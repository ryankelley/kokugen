using System;
using System.Linq;
using FubuMVC.Core.View;
using Kokugen.Core.Attributes;
using Kokugen.Core.Membership.Services;
using Kokugen.Core.Services;
using Kokugen.Web.Conventions;

namespace Kokugen.Web.Actions.Project.Manage.Users.Add
{
    public class AddUserToProjectAction
    {
        private readonly IUserService _userService;
        private readonly IRolesService _rolesService;
        private readonly IProjectService _projectService;

        public AddUserToProjectAction(IUserService userService, 
            IRolesService rolesService,
            IProjectService projectService)
        {
            _userService = userService;
            _rolesService = rolesService;
            _projectService = projectService;
        }

        public AddUserToProjectModel Query(AddUserToProjectRequest request)
        {
            return new AddUserToProjectModel(){ProjectId = request.Id};
        }

        public AjaxResponse Command(AddUserToProjectModel model)
        {
            var user = _userService.Retrieve(model.User);
            var role = _rolesService.Retrieve(model.Role);

            user.AddRole(role);

            var project = _projectService.GetProjectFromId(model.ProjectId);

            project.AddUser(user);

            var validation = _projectService.SaveProject(project);

            if (validation.IsValid())
                return new AjaxResponse() {Success = true, Item = "User has been add to the project"};

            return new AjaxResponse() {Item = validation.AllMessages.Select(x => x.Message)};
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