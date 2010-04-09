using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Security;
using Kokugen.Core.Membership.Security;
using Kokugen.Core.Membership.Services;

namespace Kokugen.Core.Membership.Abstractions.ASP_NET
{
    public class AspNetRoleProviderWrapper : IRolesService
    {
        private readonly RoleProvider _roleProvider;

        public AspNetRoleProviderWrapper(RoleProvider roleProvider)
        {
            _roleProvider = roleProvider;

        }

        #region IRolesService Members

        public IEnumerable<string> FindAll()
        {
            return _roleProvider.GetAllRoles();
        }

        public IEnumerable<string> FindByUserName(IUser userName)
        {
            return _roleProvider.GetRolesForUser(userName.UserName);
        }

        public IEnumerable<string> FindUserNamesByRole(IRole roleName)
        {
            return _roleProvider.GetUsersInRole(roleName.Role);
        }

        public void CreateIfMissing(IRole roleName)
        {
            if(!_roleProvider.RoleExists(roleName.Role))
                _roleProvider.CreateRole(roleName.Role);
        }

        public void AddToRole(IUser user, IRole roleName)
        {
            _roleProvider.AddUsersToRoles(new[] { user.UserName }, new[] { roleName.Role });
        }


        public void RemoveFromRole(IUser user, IRole roleName)
        {
            _roleProvider.RemoveUsersFromRoles(new[] { user.UserName }, new[] { roleName.Role });
        }

        public bool IsInRole(IUser user, IRole roleName)
        {
            return _roleProvider.IsUserInRole(user.UserName, roleName.Role);
        }

        public bool IsInRole(string name, string role)
        {
            throw new NotImplementedException();
        }

        public void CreateIfMissing(string administrator)
        {
            throw new NotImplementedException();
        }

        public void AddToRole(string kokugenadmin, string administrator)
        {
            throw new NotImplementedException();
        }

        public void AddUserToRoles(string name, string reader)
        {
            throw new NotImplementedException();
        }

        public void Create(IRole roleName)
        {
            _roleProvider.CreateRole(roleName.Role);
        }

        public void Delete(IRole roleName)
        {
            _roleProvider.DeleteRole(roleName.Role, false);
        }

        #endregion
    }
}