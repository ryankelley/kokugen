using System;
using System.Security.Principal;
using System.Threading;
using System.Web;
using Kokugen.Core.Membership.Services;

namespace Kokugen.Core.Membership.Security
{
    public class FubuPrincipal : IPrincipal
    {
        private readonly User _user;
        private readonly IRoleService _roleService;
        private readonly IIdentity _identity;
        private readonly Guid _userId;

        public FubuPrincipal(IIdentity identity)
        {
            _identity = identity;
            _userId = new Guid(_identity.Name);
        }

        public FubuPrincipal(IIdentity identity, User user, IRoleService roleService)
            : this(identity)
        {
            _user = user;
            _roleService = roleService;
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
            if(_roleService != null && _user != null)
                return _roleService.IsInRole(_user,role);
            return false;
        }

        public IIdentity Identity
        {
            get { return _identity; }
        }

        #endregion
    }
}