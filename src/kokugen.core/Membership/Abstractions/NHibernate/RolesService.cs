using System;
using System.Collections.Generic;
using Kokugen.Core.Membership.Security;
using Kokugen.Core.Membership.Services;

namespace Kokugen.Core.Membership.Abstractions.NHibernate
{
    public class RolesService: IRolesService
    {
        public void Create(IRole roleName)
        {
            throw new NotImplementedException();
        }

        public void CreateIfMissing(IRole roleName)
        {
            throw new NotImplementedException();
        }

        public void AddToRole(IUser userName, IRole roleName)
        {
            throw new NotImplementedException();
        }

        public void RemoveFromRole(IUser userName, IRole roleName)
        {
            throw new NotImplementedException();
        }

        public void Delete(IRole roleName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> FindAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> FindByUserName(IUser userName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> FindUserNamesByRole(IRole roleName)
        {
            throw new NotImplementedException();
        }

        public bool IsInRole(IUser userName, IRole roleName)
        {
            throw new NotImplementedException();
        }

        public bool IsInRole(string name, string role)
        {
            throw new NotImplementedException();
        }

        public void CreateIfMissing(string administrator)
        {
            throw new NotImplementedException();
        }

        public void AddUserToRole(string name, string reader)
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
    }
}