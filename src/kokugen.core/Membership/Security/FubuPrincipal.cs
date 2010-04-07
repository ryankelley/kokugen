using System;
using System.Security.Principal;
using System.Threading;
using System.Web;
using Kokugen.Core.Domain;

namespace Kokugen.Core.Membership.Security
{
    public class FubuPrincipal : IPrincipal
    {
        private readonly IIdentity _identity;
        private readonly Guid _userId;
        private User _user;

        public FubuPrincipal(IIdentity identity)
        {
            _identity = identity;
            _userId = new Guid(_identity.Name);
        }

        public FubuPrincipal(IIdentity identity, User user)
            : this(identity)
        {
            _user = user;
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
            if(_user != null)
                return _user.IsInRole(role);
            return false;
        }

        public IIdentity Identity
        {
            get { return _identity; }
        }

        #endregion
    }
}