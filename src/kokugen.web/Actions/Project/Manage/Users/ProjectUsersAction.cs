using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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

        public ProjectUsersAction(IProjectService projectService, IUrlRegistry urlRegistry)
        {
            _projectService = projectService;
            _urlRegistry = urlRegistry;
        }

        public ProjectUsersModel Query(ProjectUsersRequest request)
        {
            var project = _projectService.GetProjectFromId(request.Id);

            var owner = Mapper.Map<User, ProjectUserDTO>(project.Owner);
            owner.IsOwner = true;

            IList<ProjectUserDTO> users = Mapper.Map<List<User>, List<ProjectUserDTO>>(project.GetUsers()
                                                                                   .Except(new[] {project.Owner}).
                                                                                   ToList());

            users.Insert(0, owner);

            var roles = project.GetRoles()
                .Select(x => new RoleDTO()
                                 {
                                     Id = x.Id,
                                     Name = x.Name
                                 });

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
        public IEnumerable<RoleDTO> Roles { get; set; }
    }

    public class ProjectUserDTO
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public bool IsOwner { get; set; }
        public string GravatarHash { get; set; }
    }

    public class RoleDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class ProjectUsers : FubuPage<ProjectUsersModel>
    {
    }
}