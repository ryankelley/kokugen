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
        void Create(T roleName);
        void CreateIfMissing(T roleName);
        void AddToRole(IUser userName, T roleName);
        void RemoveFromRole(IUser user, T role);
        void Delete(T roleName);

        IEnumerable<string> FindUserNamesByRole(T roleName);
        bool IsInRole(IUser userName, T roleName);
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
        void AddToRole(string kokugenadmin, string administrator);
        void AddUserToRoles(string name, string reader);
    }

}