using System;
using Kokugen.Web.Conventions;

namespace Kokugen.Web.Actions.Project.Manage.Users.Roles
{
    public class RemoveFromRoleAction
    {
        public AjaxResponse Remove(RemoveFromRoleModel model)
        {
            return new AjaxResponse(){Success = true, Item = "User has been remove from role"};
        }
    }

    public class RemoveFromRoleModel : IRequestById
    {
        public Guid Id { get; set; }
    }
}