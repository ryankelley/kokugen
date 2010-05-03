using System;
using Kokugen.Web.Conventions;

namespace Kokugen.Web.Actions.Project.Manage.Users.Roles
{
    public class AddToRoleAction
    {
        public AjaxResponse Command(AddUserToRoleModel model)
        {
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