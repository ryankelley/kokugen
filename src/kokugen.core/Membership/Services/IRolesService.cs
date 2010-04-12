using System;
using System.Collections.Generic;
using System.Web.Security;
using Kokugen.Core.Membership.Security;
using Kokugen.Core.Persistence.Repositories;
using Kokugen.Core.Validation;

namespace Kokugen.Core.Membership.Services
{

    public interface IRolesService
    {
        void Create(IRole roleName);
        void AddToRole(IUser userName, string roleName);
        void RemoveFromRole(IUser userName, string roleName);
        void Delete(IRole roleName);

        IEnumerable<string> FindAll();
        IEnumerable<string> FindByUserName(IUser userName);
        IEnumerable<string> FindUserNamesByRole(string roleName);
        bool IsInRole(IUser userName, string roleName);

    }

}