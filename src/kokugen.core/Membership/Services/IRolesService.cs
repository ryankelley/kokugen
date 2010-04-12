using System;
using System.Collections.Generic;
using System.Web.Security;
using Kokugen.Core.Membership.Security;
using Kokugen.Core.Persistence.Repositories;
using Kokugen.Core.Validation;

namespace Kokugen.Core.Membership.Services
{
    public interface IRolesService<T> : IRolesService where T : IRole
    {
    }

    public interface IRolesService
    {
        void Create(IRole roleName);
        void CreateIfMissing(IRole roleName);
        void AddToRole(IUser userName, IRole roleName);
        void RemoveFromRole(IUser userName, IRole roleName);
        void Delete(IRole roleName);

        IEnumerable<string> FindAll();
        IEnumerable<string> FindByUserName(IUser userName);
        IEnumerable<string> FindUserNamesByRole(IRole roleName);
        bool IsInRole(IUser userName, IRole roleName);
        bool IsInRole(string name, string role);
        void CreateIfMissing(string administrator);
        void AddUserToRole(string name, string reader);
    }

}