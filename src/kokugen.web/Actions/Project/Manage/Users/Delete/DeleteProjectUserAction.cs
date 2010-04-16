using System;
using System.Collections.Generic;
using FubuMVC.Core.View;
using Kokugen.Core.Attributes;
using Kokugen.Core.Domain;
using Kokugen.Core.Membership.Services;
using Kokugen.Core.Services;
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
            var users = _projectService.GetProjectFromId(request.Id).GetUsers();


            return new DeleteProjectUserModel() {ProjectId = request.Id, Users = users};
        }

        public AjaxResponse Remove(DeleteProjectUserModel model)
        {
            var project = _projectService.GetProjectFromId(model.ProjectId);

            project.RemoveUser(_userService.Retrieve(model.UserId));

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
        public Guid UserId { get; set; }
        public IEnumerable<User> Users { get; set; }
    }

    public class DeleteProjectUser : FubuPage<DeleteProjectUserModel>
    {
    }
}