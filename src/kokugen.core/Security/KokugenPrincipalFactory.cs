using System;
using System.Security.Principal;
using FubuMVC.Core.Security;
using Kokugen.Core.Services;

namespace Kokugen.Core.Security
{
    public class KokugenPrincipalFactory : IPrincipalFactory
    {
        private readonly IUserService _userService;

        public KokugenPrincipalFactory(IUserService userService)
        {
            _userService = userService;
        }

        public IPrincipal CreatePrincipal(IIdentity identity)
        {
            var user = _userService.Retrieve(new Guid(identity.Name));
            return new KokugenPrincipal(identity,user);
        }
    }
}