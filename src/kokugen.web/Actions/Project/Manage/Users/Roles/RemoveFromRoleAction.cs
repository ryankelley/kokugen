using System;
using Kokugen.Core.Membership.Services;
using Kokugen.Web.Conventions;

namespace Kokugen.Web.Actions.Project.Manage.Users.Roles
{
    public class RemoveFromRoleAction
    {
        private readonly IUserService _userService;
        private readonly IRolesService _rolesService;

        public RemoveFromRoleAction(IUserService userService, IRolesService rolesService)
        {
            _userService = userService;
            _rolesService = rolesService;
        }

        public AjaxResponse Remove(RemoveFromRoleModel model)
        {
            var user = _userService.GetUserById(model.UserId);

            user.RemoveRole(_rolesService.Retrieve(model.RoleId));

            _userService.Update(user);

            return new AjaxResponse(){Success = true, Item = "User has been remove from role"};
        }
    }

    public class RemoveFromRoleModel : IRequestById
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}