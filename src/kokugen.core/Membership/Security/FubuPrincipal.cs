using System;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Security;
using Kokugen.Core.Domain;
using Kokugen.Core.Membership.Services;

namespace Kokugen.Core.Membership.Security
{
    public class FubuPrincipal : IPrincipal
    {
        private readonly Domain.User _user;
        private readonly IRolesService _rolesService;
        private readonly IIdentity _identity;

        private FubuPrincipal(IIdentity identity)
        {
            _identity = identity;
        }

        public FubuPrincipal(IIdentity identity, Domain.User user)
            : this(identity)
        {
            _user = user;
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
            if (_user != null)
                return _user.HasPermission(role);//.IsInRole(role));
            return false;
        }

        public IIdentity Identity
        {
            get { return _identity; }
        }

        public User User
        {
            get {
                return _user;
            }

        }

        #endregion
    }
}