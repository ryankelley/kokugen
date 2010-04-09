using System;
using System.Collections.Generic;
using Kokugen.Core.Membership.Services;

namespace Kokugen.Core.Membership.Abstractions.NHibernate
{
    public class RolesService : IRolesService
    {
        public void Create(string roleName)
        {
            throw new NotImplementedException();
        }

        public void CreateIfMissing(string roleName)
        {
            throw new NotImplementedException();
        }

        public void AddToRole(string userName, string roleName)
        {
            throw new NotImplementedException();
        }

        public void AddUserToRoles(string userName, params string[] roles)
        {
            throw new NotImplementedException();
        }

        public void RemoveFromRole(string userName, string roleName)
        {
            throw new NotImplementedException();
        }

        public void Delete(string roleName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> FindAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> FindByUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> FindUserNamesByRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public bool IsInRole(string userName, string roleName)
        {
            throw new NotImplementedException();
        }
    }
}