using System;
using Kokugen.Core;
using Kokugen.Core.Attributes;
using Kokugen.Core.Domain;
using Kokugen.Core.Membership.Services;

namespace Kokugen.Web.Startables
{
    public class SetupSuperUser : IStartable 
    {
        private readonly IUserService _userService;
        private readonly IRolesService _rolesService;
        private User _user;

        public SetupSuperUser(IUserService userService, IRolesService rolesService)
        {
            _userService = userService;
            _rolesService = rolesService;
        }

        public void Start()
        {
            addDefaultAdmin();
        }


        private void addDefaultAdmin()
        {
            _user = new SuperUser("KokugenAdmin", "KokugenAdmin@Kokugen.com", "K0kugen@dmin");
            _userService.Create(_user);
        }
    }
}