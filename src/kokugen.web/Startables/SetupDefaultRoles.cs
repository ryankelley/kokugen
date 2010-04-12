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
            //addDefaultRoles();
        }

        private void addDefaultRoles()
        {

            _rolesService.Create(new Core.Domain.Role("Administrator"));
            _rolesService.Create(new Core.Domain.Role("Coordinator"));
            _rolesService.Create(new Core.Domain.Role("Contributor"));
            _rolesService.Create(new Core.Domain.Role("Reader"));

            var login = _userService.GetUserByLogin("KokugenAdmin");

            if (!_rolesService.IsInRole(login, "Administrator"))
            {
                _rolesService.AddToRole(login, "Administrator");
            }
        }

        private void addDefaultAdmin()
        {
            _userService.Create(new Core.Domain.User("KokugenAdmin", "KokugenAdmin@Kokugen.com", "K0kugen@dmin"));
        }
    }

    [DebugOnly]
    public class StubLotsOfUsers //: IStartable
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
            var user = new Core.Domain.User(userName, userName + "@" + userName + ".com", "F@keUser");
            _userService.Create(user);

            //if (!_rolesService.IsInRole(user, "Reader"))
            //{
            //    _rolesService.AddToRole(user, "Reader");
            //}
        }
    }
}