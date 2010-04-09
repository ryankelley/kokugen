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
    public interface IUserService<T>: IUserService where T : IUser
    {
    }

    public interface IUserService
    {
        void Update(IUser user);
        void Delete(IUser user);
        IUser Retrieve(object id);

        IUser GetUserByLogin(string name);
        IUser GetUserByEmail(string email);
        IPagedList<IUser> FindAll(int pageIndex, int pageSize);

        int TotalUsers { get; }
        int UsersOnline { get; }

        MembershipCreateStatus CreateUser(string userName, string password, string email, bool isAppoved);
        MembershipCreateStatus CreateUser(string userName, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved);
        MembershipCreateStatus CreateUser(string userName, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object key);
    }
}