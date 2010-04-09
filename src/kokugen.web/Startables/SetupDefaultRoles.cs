using System;
using Kokugen.Core;
using Kokugen.Core.Attributes;
using Kokugen.Core.Membership.Services;

namespace Kokugen.Web.Startables
{
    public class SetupDefaultRoles : IStartable 
    {
        private readonly IUserService _userService;
        private readonly IRolesService _rolesService;

        public SetupDefaultRoles(IUserService userService, IRolesService rolesService)
        {
            _userService = userService;
            _rolesService = rolesService;
        }

        public void Start()
        {
            addDefaultAdmin();
            addDefaultRoles();
        }

        private void addDefaultRoles()
        {

            _rolesService.CreateIfMissing("Administrator");
            _rolesService.CreateIfMissing("Coordinator");
            _rolesService.CreateIfMissing("Contributor");
            _rolesService.CreateIfMissing("Reader");

            var admin = _userService.GetUserByLogin("KokugenAdmin");
            if (!_rolesService.IsInRole(admin, "Administrator"))
            {
                _rolesService.AddToRole(admin, "Administrator");
            }
        }

        private void addDefaultAdmin()
        {
            _userService.CreateUser("KokugenAdmin", "K0kugen@dmin", "admin@kokugen.com", true);
        }
    }

    [DebugOnly]
    public class StubLotsOfUsers : IStartable
    {
        private readonly IUserService _userService;
        private readonly IRolesService _rolesService;

        public StubLotsOfUsers(IUserService userService, IRolesService rolesService)
        {
            _userService = userService;
            _rolesService = rolesService;
        }

        public void Start()
        {
           AddFakeUser("George");
           AddFakeUser("Jane");
           AddFakeUser("Jim");
           AddFakeUser("Jeff");
           AddFakeUser("Fred");
           AddFakeUser("Bill");
           AddFakeUser("Tom");
           AddFakeUser("Chris");
           AddFakeUser("Corey");
           AddFakeUser("Tony"); 
           AddFakeUser("Dave");
        }

        private void AddFakeUser(string userName)
        {
            _userService.CreateUser(userName, "F@keUser", userName + "@" + userName + ".com", true);
        }
    }
}