using System;
using System.Security.Principal;
using FubuMVC.Core.Security;
using Kokugen.Core.Membership.Services;

namespace Kokugen.Core.Membership.Security
{
    public class FubuPrincipalFactory : IPrincipalFactory
    {
  
        private readonly IRolesService _rolesService;

        public FubuPrincipalFactory(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        public IPrincipal CreatePrincipal(IIdentity identity)
        {
            return new FubuPrincipal(identity,_rolesService);
        }
    }
}