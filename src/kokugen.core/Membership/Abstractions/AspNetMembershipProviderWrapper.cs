using System;
using System.Collections.Generic;
using System.Web.Security;
using Kokugen.Core.Membership.Security;
using Kokugen.Core.Membership.Services;
using Kokugen.Core.Membership.Settings;
using Kokugen.Core.Validation;
using PagedList;

namespace Kokugen.Core.Membership.Abstractions
{
    public class AspNetMembershipProviderWrapper : IUserService, IMembershipValidator, IPasswordService
    {
        private readonly MembershipProvider _provider;
        private readonly MembershipSettingsBag _settingsBag;

        public AspNetMembershipProviderWrapper(MembershipProvider provider, 
            MembershipSettingsBag settingsBag)
        {
            _provider = provider;
            _settingsBag = settingsBag;
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email, bool isApproved)
        {
            return CreateUser(userName, password, email, null, null, isApproved, null);
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email,
            string passwordQuestion,
            string passwordAnswer,
            bool isApproved)
        {
            return CreateUser(userName, password, email, passwordQuestion, passwordAnswer, isApproved, null);
        }

        public MembershipCreateStatus CreateUser(string userName,
            string password,
            string email,
            string passwordQuestion,
            string passwordAnswer,
            bool isApproved,
            object key)
        {


            MembershipCreateStatus status;
            _provider.CreateUser(userName, password, email, passwordQuestion, passwordAnswer, isApproved, key,
                                 out status);
            return status;
        }

        public void Update(IUser user)
        {
            var membershipUser = _provider.GetUser(user.UserName, false);
            membershipUser.Email = user.Email;
            _provider.UpdateUser(membershipUser);
        }

        public void Delete(IUser user)
        {
            _provider.DeleteUser(user.UserName, true);
        }

        public IUser Retrieve(object Id)
        {
            var membershipUser = _provider.GetUser(Id, false);
            return new User(membershipUser.UserName, membershipUser.Email, membershipUser.ProviderUserKey);
        }

        public IUser GetUserByLogin(string name)
        {
            var membershipUser = _provider.GetUser(name, false);
            return new User(membershipUser.UserName, membershipUser.Email, membershipUser.ProviderUserKey);
        }

        public IUser GetUserByEmail(string email)
        {
            var membershipUser = _provider.GetUser(_provider.GetUserNameByEmail(email), false);
            return new User(membershipUser.UserName, membershipUser.Email, membershipUser.ProviderUserKey);
        }

        public IPagedList<IUser> FindAll(int pageIndex, int pageSize)
        {
            // get one page of users
            int totalUserCount;
            var usersCollection = _provider.GetAllUsers(pageIndex, pageSize, out totalUserCount);

            // convert from MembershipUserColletion to PagedList<MembershipUser> and return
            var converter = new MembershipUserCollectionToIUserConverter();
            var usersList = converter.ConvertTo<IEnumerable<IUser>>(usersCollection);
            var usersPagedList = new StaticPagedList<IUser>(usersList, pageIndex, pageSize, totalUserCount);
            return usersPagedList;
        }

        public int TotalUsers
        {
            get
            {
                int totalUsers;
                _provider.GetAllUsers(1, 1, out totalUsers);
                return totalUsers;
            }
        }

        public int UsersOnline
        {
            get
            {
                return _provider.GetNumberOfUsersOnline();
            }
        }

        public bool ValidateUser(string userName, string password)
        {
           return  _provider.ValidateUser(userName, password);
        }

        #region IPasswordService Members

        public void Unlock(IUser user)
        {
            _provider.UnlockUser(user.UserName);
        }

        public void ChangePassword(IUser user, string oldPassword, string newPassword)
        {
            _provider.ChangePassword(user.UserName, oldPassword, newPassword);
        }

        public void ChangePasswordQuestionAndAnswer(IUser user, string password, string question, string answer)
        {
            _provider.ChangePasswordQuestionAndAnswer(user.UserName, password, question, answer);
        }

        public string GetPassword(IUser user, string passwordAnswer)
        {
            return _provider.GetPassword(user.UserName, passwordAnswer);
        }

        public string GetPassword(IUser user)
        {
            return _provider.GetUser(user.UserName, false).GetPassword();
        }

        public string ResetPassword(IUser user, string passwordAnswer)
        {
            return _provider.ResetPassword(user.UserName, passwordAnswer);
        }

        public string ResetPassword(IUser user)
        {
            return _provider.GetUser(user.UserName, false).ResetPassword();
        }

        #endregion
    }
}