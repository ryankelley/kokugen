using System;
using System.Security.Principal;
using FubuMVC.Core.Security;
using Kokugen.Core.Membership.Services;

namespace Kokugen.Core.Membership.Security
{
    public class FubuPrincipalFactory : IPrincipalFactory
    {
        private readonly IUserService _userService;

        public FubuPrincipalFactory(IUserService userService)
        {
            _userService = userService;
        }

        public IPrincipal CreatePrincipal(IIdentity identity)
        {
            var user = _userService.Retrieve(new Guid(identity.Name));
            return new FubuPrincipal(identity,user);
        }
    }
}