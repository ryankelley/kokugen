using System;
using System.Linq;
using AutoMapper;
using FubuMVC.Core;
using Kokugen.Core.Domain;
using Kokugen.Core.Membership.Services;
using Kokugen.Web.Conventions;

namespace Kokugen.Web.Actions.Project.Manage.Users.Roles
{
    public class GetUsersAction
    {
        private readonly IRolesService _rolesService;

        public GetUsersAction(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        public AjaxResponse Query(GetUsersModel model)
        {
            var role = _rolesService.Retrieve(model.RoleId);

            var users = Mapper.Map<User[], ProjectUserDTO[]>(role.GetUsers().ToArray());

            return new AjaxResponse() {Success = true, Item = users};
        }
    }

    public class GetUsersModel : IRequestById
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
    }
}