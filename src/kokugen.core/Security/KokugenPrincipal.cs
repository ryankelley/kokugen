using System;
using System.Security.Principal;
using System.Threading;
using System.Web;
using Kokugen.Core.Domain;

namespace Kokugen.Core.Security
{
    public class KokugenPrincipal : IPrincipal
    {
        private readonly IIdentity _identity;
        private readonly Guid _userId;
        private User _user;

        public KokugenPrincipal(IIdentity identity)
        {
            _identity = identity;
            _userId = new Guid(_identity.Name);
        }

        public KokugenPrincipal(IIdentity identity, User user)
            : this(identity)
        {
            _user = user;
        }

        public Guid UserId
        {
            get { return _userId; }
        }
        
        public static KokugenPrincipal Current
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    return HttpContext.Current.User as KokugenPrincipal;
                }
                return Thread.CurrentPrincipal as KokugenPrincipal;
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