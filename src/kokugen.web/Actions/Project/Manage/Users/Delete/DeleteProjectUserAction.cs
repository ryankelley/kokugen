using System;
using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core.View;
using Kokugen.Core.Attributes;
using Kokugen.Core.Domain;
using Kokugen.Core.Membership.Services;
using Kokugen.Core.Services;
using Kokugen.Core.Validation;
using Kokugen.Web.Conventions;

namespace Kokugen.Web.Actions.Project.Manage.Users.Delete
{
    public class DeleteProjectUserAction
    {
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;

        public DeleteProjectUserAction(IProjectService projectService, IUserService userService)
        {
            _projectService = projectService;
            _userService = userService;
        }

        public DeleteProjectUserModel Query(DeleteProjectUserRequest request)
        {
            var project = _projectService.GetProjectFromId(request.Id);

            var users = project.GetUsers().ToList();

            users.Remove(project.Owner);

            return new DeleteProjectUserModel() {ProjectId = request.Id, Users = users};
        }

        public AjaxResponse Remove(DeleteProjectUserModel model)
        {
            var project = _projectService.GetProjectFromId(model.ProjectId);

            project.RemoveUser(_userService.Retrieve(model.User));

            return new AjaxResponse(){Success = true, Item = "User has been removed from the project"};
        }
    }

    public class DeleteProjectUserRequest : IRequestById
    {
        public Guid Id { get; set; }
    }

    public class DeleteProjectUserModel
    {
        public Guid ProjectId { get; set; }

        [Required]
        public Guid User { get; set; }

        public IEnumerable<User> Users { get; set; }
    }

    public class DeleteProjectUser : FubuPage<DeleteProjectUserModel>
    {
    }
}