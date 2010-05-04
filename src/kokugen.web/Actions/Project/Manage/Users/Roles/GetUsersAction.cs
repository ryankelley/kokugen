using System;
using FubuMVC.Core;
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



            return new AjaxResponse() { Success = true};
        }
    }

    public class GetUsersModel : IRequestById
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
    }
}