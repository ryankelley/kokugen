using System;
using FubuMVC.Core;
using Kokugen.Web.Conventions;

namespace Kokugen.Web.Actions.Project.Manage.Users.Roles
{
    public class GetUsersAction
    {
        public AjaxResponse Query(GetUsersModel model)
        {
            return new AjaxResponse() { Success = true};
        }
    }

    public class GetUsersModel : IRequestById
    {
        public Guid Id { get; set; }
        //[QueryString]
        public Guid RoleId { get; set; }
    }
}