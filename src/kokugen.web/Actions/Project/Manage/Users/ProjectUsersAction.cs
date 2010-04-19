using System;
using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core.Urls;
using FubuMVC.Core.View;
using Kokugen.Core.Domain;
using Kokugen.Core.Membership.Services;
using Kokugen.Core.Services;
using Kokugen.Web.Actions.Project.Manage.Users.Delete;
using Kokugen.Web.Conventions;

namespace Kokugen.Web.Actions.Project.Manage.Users
{
    public class ProjectUsersAction
    {
        private readonly IProjectService _projectService;
        private readonly IRolesService _rolesService;
        private readonly IUrlRegistry _urlRegistry;

        public ProjectUsersAction(IProjectService projectService, IRolesService rolesService, IUrlRegistry urlRegistry)
        {
            _projectService = projectService;
            _rolesService = rolesService;
            _urlRegistry = urlRegistry;
        }

        public ProjectUsersModel Query(ProjectUsersRequest request)
        {
            var project = _projectService.GetProjectFromId(request.Id);

            var owner = new ProjectUserDTO()
                            {
                                Id = project.Owner.Id,
                                DisplayName = project.Owner.DisplayName(),
                                GravatarHash = project.Owner.GravatarHash,
                                IsOwner = true,
                                ProjectId = project.Id
                            };

            IList<ProjectUserDTO> users = project.GetUsers()
                .Except(new[] {project.Owner})
                .Select(x => new ProjectUserDTO()
                                 {
                                     Id = x.Id,
                                     DisplayName = x.DisplayName(),
                                     GravatarHash = x.GravatarHash,
                                     DeleteUrl = _urlRegistry.UrlFor(new DeleteProjectUserModel()),
                                     ProjectId = project.Id
                                 }).ToList();

            users.Insert(0, owner);

            var roles = _rolesService.FindAll().ToList();

            roles.Add(new Role("Test"){Id = Guid.NewGuid()});

            return new ProjectUsersModel(){ProjectId = request.Id, Users = users, Roles = roles};
        }

        public AjaxResponse Command(ProjectUsersModel model)
        {
            return new AjaxResponse();
        }
    }

    public class ProjectUsersRequest : IRequestById
    {
        public Guid Id { get; set; }
    }

    public class ProjectUsersModel : ProjectBaseViewModel
    {
        public Guid Id { get; set; }
        public IEnumerable<ProjectUserDTO> Users { get; set; }
        public IEnumerable<Role> Roles { get; set; }
    }

    public class ProjectUserDTO
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public bool IsOwner { get; set; }
        public string GravatarHash { get; set; }
        public string DeleteUrl { get; set;}

        public Guid ProjectId { get; set; }
    }

    public class ProjectUsers : FubuPage<ProjectUsersModel>
    {
    }
}