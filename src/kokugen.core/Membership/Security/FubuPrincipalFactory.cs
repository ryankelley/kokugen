using System;
using System.Security.Principal;
using FubuMVC.Core.Security;
using Kokugen.Core.Membership.Services;

namespace Kokugen.Core.Membership.Security
{
    public class FubuPrincipalFactory : IPrincipalFactory
    {
        private readonly IUserService _userService;
        private readonly IRolesService _rolesService;

        public FubuPrincipalFactory(IUserService userService, IRolesService rolesService)
        {
            _userService = userService;
            _rolesService = rolesService;
        }

        public IPrincipal CreatePrincipal(IIdentity identity)
        {
            return new FubuPrincipal(identity,_userService.Retrieve(identity.Name),_rolesService);
        }
    }
}