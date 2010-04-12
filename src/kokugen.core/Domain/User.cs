using System;
using System.Collections.Generic;
using Kokugen.Core.Membership.Security;

namespace Kokugen.Core.Domain
{
    public class User : Entity, IUser
    {
        private IList<Role> _roles = new List<Role>();

        protected User()
        {}

        public User(string userName, string email)
        {
            UserName = userName;
            Email = email;
        }


        #region Implementation of IUser

        public virtual string UserName { get; protected set; }
        public virtual string Email { get; set; }

        public virtual object ProviderUserKey
        {
            get { return Id; }
        }

        #endregion

        public virtual IEnumerable<Role> GetRoles()
        {
            return _roles;
        }
    }
}