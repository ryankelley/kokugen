using System;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Security;
using Kokugen.Core.Membership.Services;

namespace Kokugen.Core.Membership.Security
{
    public class FubuPrincipal : IPrincipal
    {
        private readonly MembershipUser _user;
        private readonly IRolesService _rolesService;
        private readonly IIdentity _identity;

        private FubuPrincipal(IIdentity identity)
        {
            _identity = identity;
        }

        public FubuPrincipal(IIdentity identity, IRolesService rolesService)
            : this(identity)
        {
            _rolesService = rolesService;
        }
        
        public static FubuPrincipal Current
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    return HttpContext.Current.User as FubuPrincipal;
                }
                return Thread.CurrentPrincipal as FubuPrincipal;
            }
        }

        #region IPrincipal Members

        public bool IsInRole(string role)
        {
            if(_rolesService != null)
                return _rolesService.IsInRole(_identity.Name,role);
            return false;
        }

        public IIdentity Identity
        {
            get { return _identity; }
        }

        #endregion
    }
}