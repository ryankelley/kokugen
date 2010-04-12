using System;
using Kokugen.Core;
using Kokugen.Core.Attributes;
using Kokugen.Core.Domain;
using Kokugen.Core.Membership.Services;

namespace Kokugen.Web.Startables
{
    public class SetupDefaultRoles : IStartable 
    {
        private readonly IUserService _userService;
        private readonly IRolesService _rolesService;
        private User _user;

        public SetupDefaultRoles(IUserService userService, IRolesService rolesService)
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
            var role = new Role("Administrator");
            _rolesService.Create(role);

            _user = new User("KokugenAdmin", "KokugenAdmin@Kokugen.com", "K0kugen@dmin");
            _user.AddRole(role);
            _userService.Create(_user);
        }
    }

    [DebugOnly]
    public class StubLotsOfUsers : IStartable
    {
        private readonly IUserService _userService;
        private readonly IRolesService _rolesService;
        private Role _role;

        public StubLotsOfUsers(IUserService userService, IRolesService rolesService)
        {
            _userService = userService;
            _rolesService = rolesService;
        }

        public void Start()
        {
            _role = new Role("Reader");
            _rolesService.Create(_role);
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
            var user = new Core.Domain.User(userName, userName + "@" + userName + ".com", "F@keUser");
            user.AddRole(_role);
            _userService.Create(user);

            //if (!_rolesService.IsInRole(user, "Reader"))
            //{
            //    _rolesService.AddToRole(user, "Reader");
            //}
        }
    }
}