using System;
using System.Collections.Generic;
using Kokugen.Core.Membership.Security;

namespace Kokugen.Core.Domain
{
    public class Role : Entity, IRole 
    {
        private IList<User> _users = new List<User>();

        protected Role()
        {
        }

        public Role(string name)
        {
            Name = name;
        }

        #region Implementation of IRole

        public virtual string Name { get; protected set; }

        #endregion

        public virtual IEnumerable<User> GetUsers()
        {
            return _users;
        }
    }
}