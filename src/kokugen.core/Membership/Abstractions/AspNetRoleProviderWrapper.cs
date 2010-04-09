using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Security;
using Kokugen.Core.Membership.Services;

namespace Kokugen.Core.Membership.Abstractions
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

        public IEnumerable<string> FindByUserName(string userName)
        {
            return _roleProvider.GetRolesForUser(userName);
        }

        public IEnumerable<string> FindUserNamesByRole(string roleName)
        {
            return _roleProvider.GetUsersInRole(roleName);
        }

        public void CreateIfMissing(string roleName)
        {
            if(!_roleProvider.GetAllRoles().Contains(roleName))
                _roleProvider.CreateRole(roleName);
        }

        public void AddToRole(string user, string roleName)
        {
            _roleProvider.AddUsersToRoles(new[] { user }, new[] { roleName });
        }

        public void AddUserToRoles(string userName, params string[] roles)
        {
            _roleProvider.AddUsersToRoles(new[] { userName }, roles);
        }

        public void RemoveFromRole(string user, string roleName)
        {
            _roleProvider.RemoveUsersFromRoles(new[] { user }, new[] { roleName });
        }

        public bool IsInRole(string user, string roleName)
        {
            return _roleProvider.IsUserInRole(user, roleName);
        }

        public void Create(string roleName)
        {
            _roleProvider.CreateRole(roleName);
        }

        public void Delete(string roleName)
        {
            _roleProvider.DeleteRole(roleName, false);
        }

        #endregion
    }
}