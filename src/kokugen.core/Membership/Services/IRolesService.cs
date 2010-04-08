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
        void Create(string roleName);
        void CreateIfMissing(string roleName);
        void AddToRole(MembershipUser user, string roleName);
        void RemoveFromRole(MembershipUser user, string roleName);
        void Delete(string roleName);

        IEnumerable<string> FindAll();
        IEnumerable<string> FindByUser(MembershipUser user);
        IEnumerable<string> FindByUserName(string userName);
        IEnumerable<string> FindUserNamesByRole(string roleName);
        bool IsInRole(MembershipUser user, string roleName);
    }

}