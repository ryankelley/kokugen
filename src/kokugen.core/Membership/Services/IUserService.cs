#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using Kokugen.Core.Membership.Security;
using Kokugen.Core.Persistence.Repositories;
using Kokugen.Core.Validation;
using PagedList;

#endregion

namespace Kokugen.Core.Membership.Services
{
    public interface IUserService
    {
        void Update(MembershipUser user);
        void Delete(MembershipUser user);
        MembershipUser Retrieve(Guid id);

        MembershipUser GetUserByLogin(string name);
        MembershipUser GetUserByEmail(string email);
        IPagedList<MembershipUser> FindAll(int pageIndex, int pageSize);

        int TotalUsers { get; }
        int UsersOnline { get; }

        MembershipCreateStatus CreateUser(string userName, string password, string email, bool isAppoved);
        MembershipCreateStatus CreateUser(string userName, string password, string email,string passwordQuestion,string passwordAnswer,bool isApproved);
        MembershipCreateStatus CreateUser(string userName,string password,string email,string passwordQuestion,string passwordAnswer,bool isApproved,object key);
    }
}