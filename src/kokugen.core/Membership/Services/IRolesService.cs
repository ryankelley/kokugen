using System;
using System.Collections.Generic;
using System.Web.Security;
using Kokugen.Core.Domain;
using Kokugen.Core.Membership.Security;
using Kokugen.Core.Persistence.Repositories;
using Kokugen.Core.Validation;

namespace Kokugen.Core.Membership.Services
{

    public interface IRolesService<ROLE, USER>where USER : IUser
            where ROLE : IRole
    {
        void Create(ROLE roleName);
        void AddUserToRole(USER userName, ROLE roleName);
        void RemoveUserFromRole(USER userName, ROLE roleName);
        void Delete(ROLE roleName);

        IEnumerable<ROLE> FindAll();
        IEnumerable<ROLE> FindByUserName(USER userName);
        IEnumerable<string> FindUserNamesByRole(ROLE roleName);
        ROLE Retrieve(object id);

        bool IsInRole(USER userName, ROLE roleName);

    }

    public interface IRolesService : IRolesService<Role,User>{}

}