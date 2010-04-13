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

        public IEnumerable<string> FindUserNamesByRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> FindUserNamesByRole(IRole roleName)
        {
            return _roleProvider.GetUsersInRole(roleName.Name);
        }

        public bool IsInRole(IUser userName, string roleName)
        {
            throw new NotImplementedException();
        }

        public void CreateIfMissing(IRole roleName)
        {
            if (!_roleProvider.RoleExists(roleName.Name))
                _roleProvider.CreateRole(roleName.Name);
        }

        public void AddToRole(IUser user, IRole roleName)
        {
            _roleProvider.AddUsersToRoles(new[] { user.UserName }, new[] { roleName.Name });
        }


        public void RemoveFromRole(IUser user, IRole roleName)
        {
            _roleProvider.RemoveUsersFromRoles(new[] { user.UserName }, new[] { roleName.Name });
        }

        public bool IsInRole(IUser user, IRole roleName)
        {
            return _roleProvider.IsUserInRole(user.UserName, roleName.Name);
        }

        public bool IsInRole(string name, string role)
        {
           return _roleProvider.IsUserInRole(name, role);
        }

        public void CreateIfMissing(string role)
        {
            if(!_roleProvider.RoleExists(role))
                _roleProvider.CreateRole(role);
        }


        public void AddUserToRole(string name, string role)
        {
            _roleProvider.AddUsersToRoles(new[] { name }, new[] { role });
        }

        public void Create(IRole roleName)
        {
            _roleProvider.CreateRole(roleName.Name);
        }

        public void AddToRole(IUser userName, string roleName)
        {
            throw new NotImplementedException();
        }

        public void RemoveFromRole(IUser userName, string roleName)
        {
            throw new NotImplementedException();
        }

        public void Delete(IRole roleName)
        {
            _roleProvider.DeleteRole(roleName.Name, false);
        }

        #endregion
    }
}