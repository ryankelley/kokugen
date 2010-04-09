using System;
using System.Web.Security;
using Kokugen.Core.Membership.Security;
using Kokugen.Core.Membership.Services;
using PagedList;

namespace Kokugen.Core.Membership.Abstractions.NHibernate
{
    public class UserService : IUserService
    {
        public void Update(IUser user)
        {
            throw new NotImplementedException();
        }

        public void Delete(IUser user)
        {
            throw new NotImplementedException();
        }

        public IUser Retrieve(object id)
        {
            throw new NotImplementedException();
        }

        public IUser GetUserByLogin(string name)
        {
            throw new NotImplementedException();
        }

        public IUser GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public IPagedList<IUser> FindAll(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public int TotalUsers
        {
            get { throw new NotImplementedException(); }
        }

        public int UsersOnline
        {
            get { throw new NotImplementedException(); }
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email, bool isAppoved)
        {
            throw new NotImplementedException();
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved)
        {
            throw new NotImplementedException();
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object key)
        {
            throw new NotImplementedException();
        }

    }
}