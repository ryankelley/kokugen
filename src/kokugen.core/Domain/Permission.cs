using System.Collections.Generic;
using System.Linq;
using Kokugen.Core.Membership;

namespace Kokugen.Core.Domain
{
    public class Permission: Entity
    {

        private IList<Role> _roles = new List<Role>();

        private int _permissionNameId;
        
        public virtual int PermissionNameId
        {
            get { return _permissionNameId; }
            set { _permissionNameId = value; }
        }

        
        public virtual PermissionName Name
        {
            get { return Enumeration.FromValue<PermissionName>(_permissionNameId); }
            set { _permissionNameId = value.Value; }
        }

        public virtual IEnumerable<Role> GetRoles()
        {
            return _roles.AsEnumerable();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Permission)) return false;

            var perm = obj as Permission;
            return perm.Name == Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public static bool operator ==(Permission left, Permission right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Permission left, Permission right)
        {
            return !Equals(left, right);
        }
    }
}