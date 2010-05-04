using System;
using Kokugen.Core.Domain;
using Kokugen.Core.Membership.Services;
using Kokugen.Web.Conventions;

namespace Kokugen.Web.Actions.Project.Manage.Users.Roles
{
    public class AddToRoleAction
    {
        private readonly IUserService _userService;
        private readonly IRolesService _rolesService;

        public AddToRoleAction(IUserService userService, IRolesService rolesService)
        {
            _userService = userService;
            _rolesService = rolesService;
        }

        public AjaxResponse Command(AddUserToRoleModel model)
        {
            var user = _userService.GetUserById(model.UserId);

            user.AddRole(_rolesService.Retrieve(model.RoleId));

            _userService.Update(user);

            return new AjaxResponse(){Success = true, Item = "User successfully added to role."};
        }
    }

    public class AddUserToRoleModel : IRequestById 
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
    }
}