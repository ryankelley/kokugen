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
        private readonly Guid _userId;

        public FubuPrincipal(IIdentity identity)
        {
            _identity = identity;
            _userId = new Guid(_identity.Name);
        }

        public FubuPrincipal(IIdentity identity, MembershipUser user, IRolesService rolesService)
            : this(identity)
        {
            _user = user;
            _rolesService = rolesService;
        }

        public Guid UserId
        {
            get { return _userId; }
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
            if(_rolesService != null && _user != null)
                return _rolesService.IsInRole(_user,role);
            return false;
        }

        public IIdentity Identity
        {
            get { return _identity; }
        }

        #endregion
    }
}