using System;
using System.Security.Principal;
using FubuMVC.Core.Security;
using Kokugen.Core.Membership.Services;

namespace Kokugen.Core.Membership.Security
{
    public class FubuPrincipalFactory : IPrincipalFactory
    {
<<<<<<< HEAD
        private readonly IUserService _userService;
        private readonly IRolesService _rolesService;

        public FubuPrincipalFactory(IUserService userService, IRolesService rolesService)
        {
            _userService = userService;
=======
  
        private readonly IRolesService _rolesService;

        public FubuPrincipalFactory(IRolesService rolesService)
        {
>>>>>>> 57439e38f03213e6fa3fc25a6ebc88976c3819e6
            _rolesService = rolesService;
        }

        public IPrincipal CreatePrincipal(IIdentity identity)
        {
            return new FubuPrincipal(identity,_rolesService);
        }
    }
}