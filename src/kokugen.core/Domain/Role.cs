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

        private IList<Permission> _permissions = new List<Permission>();

        public virtual IEnumerable<Permission> Permissions
        {
            get { return _permissions; }
        }

        public virtual void AddPermission(Permission permission)
        {
            if (!_permissions.Contains(permission))
            {
                _permissions.Add(permission);
            }
        }

        public virtual void RemovePermission(Permission permission)
        {
            if (_permissions.Contains(permission))
            {
                _permissions.Remove(permission);
            }
        }

        public virtual void RemoveAllPermissions()
        {
            _permissions.Clear();
        }
    }
}