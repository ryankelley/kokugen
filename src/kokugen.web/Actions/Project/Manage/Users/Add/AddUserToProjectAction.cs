using System;
using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core.Urls;
using FubuMVC.Core.View;
using Kokugen.Core.Attributes;
using Kokugen.Core.Domain;
using Kokugen.Core.Membership.Services;
using Kokugen.Core.Services;
using Kokugen.Core.Validation;
using Kokugen.Web.Actions.Project.Manage.Users.Delete;
using Kokugen.Web.Conventions;

namespace Kokugen.Web.Actions.Project.Manage.Users.Add
{
    public class AddUserToProjectAction
    {
        private readonly IUserService _userService;
        private readonly IUrlRegistry _urlRegistry;
        private readonly IProjectService _projectService;

        public AddUserToProjectAction(IUserService userService, 
            IUrlRegistry urlRegistry,
            IProjectService projectService)
        {
            _userService = userService;
            _urlRegistry = urlRegistry;

            _projectService = projectService;
        }

        public AddUserToProjectModel Query(AddUserToProjectRequest request)
        {
            var users = _userService.FindAll();
            var projectUsers = _projectService.GetProjectFromId(request.Id).GetUsers();
            var availableUsers = users.Except(projectUsers);

            return new AddUserToProjectModel(){ProjectId = request.Id, Users = availableUsers};
        }

        public AjaxResponse Command(AddUserToProjectModel model)
        {
            var user = _userService.Retrieve(model.User);

            var project = _projectService.GetProjectFromId(model.ProjectId);

            project.AddUser(user);

            var validation = _projectService.SaveProject(project);

            if (validation.IsValid())
                return new AjaxResponse()
                           {
                               Success = true,
                               Item =
                                   new ProjectUserDTO
                                       {
                                           Id = user.Id,
                                           ProjectId = project.Id,
                                           DisplayName = user.DisplayName(),
                                           GravatarHash = user.GravatarHash,
                                           DeleteUrl = _urlRegistry.UrlFor(new DeleteProjectUserModel())
                                       }
                           };

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

        [Required]
        public Guid User { get; set; }

        public IEnumerable<User> Users { get; set; }
    }

    public class AddUserToProject : FubuPage<AddUserToProjectModel>{}
}