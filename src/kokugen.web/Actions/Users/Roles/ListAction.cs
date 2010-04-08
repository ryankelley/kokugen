using System.Linq;
using FubuMVC.Core.View;
using Kokugen.Core.Membership.Services;

namespace Kokugen.Web.Actions.Users.Roles
{
    public class ListAction
    {
        private readonly IRolesService _rolesService;

        public ListAction(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        public RolesListModel Query(RolesListModel model)
        {
           return new RolesListModel(){Roles = _rolesService.FindAll().ToArray()};
        }
    }

    public class RolesListModel
    {
        public string[] Roles { get; set; }
    }

    public class List : FubuPage<RolesListModel>{}
}