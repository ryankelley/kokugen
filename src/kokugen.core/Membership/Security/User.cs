using System;
using System.Collections.Generic;
using System.Linq;
using Kokugen.Core.Domain;
using Kokugen.Core.Validation;

namespace Kokugen.Core.Membership.Security
{
    public class User 
    {
        private readonly IList<Role> _roles = new List<Role>();

        public virtual Guid Id { get; set; }

        public virtual string FirstName { get; set; }
        
        public virtual string LastName { get; set; }

        public virtual string HashedPassword { get; set; }
        
        public virtual string Login { get; set; }   

        public virtual string EmailAddress { get; set; }

        public virtual IEnumerable<Role> GetRoles()
        {
            return _roles;
        }

        public virtual void AddRole(Role role)
        {
            _roles.Add(role);
        }

        public virtual void AddRoles(IEnumerable<Role> roles)
        {
            roles.Each(x => _roles.Add(x));
        }

        public virtual void ClearRoles()
        {
            _roles.Clear();
        }

        public virtual void SetRoles(IEnumerable<Role> roles)
        {
            ClearRoles();
            AddRoles(roles);
        }

        public virtual bool IsInRole(string roleName)
        {
            return _roles.Where(x => x.Name == roleName).Count() > 0;
        }
    }
}