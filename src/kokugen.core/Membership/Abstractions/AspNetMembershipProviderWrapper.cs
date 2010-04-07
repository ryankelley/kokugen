using System;
using System.Collections.Generic;
using System.Web.Security;
using Kokugen.Core.Membership.Services;
using Kokugen.Core.Membership.Settings;
using Kokugen.Core.Validation;

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

        public void Update(MembershipUser user)
        {
           _provider.UpdateUser(user);
        }

        public void Delete(MembershipUser user)
        {
            _provider.DeleteUser(user.UserName, true);
        }

        public MembershipUser Retrieve(Guid Id)
        {
            return _provider.GetUser(Id, true);
        }

        public MembershipUser GetUserByLogin(string name)
        {
            return _provider.GetUser(name, true);
        }

        public MembershipUser GetUserByEmail(string email)
        {
            return _provider.GetUser(_provider.GetUserNameByEmail(email),true);
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

        public void Unlock(MembershipUser user)
        {
            _provider.UnlockUser(user.UserName);
        }

        public void ChangePassword(MembershipUser user, string oldPassword, string newPassword)
        {
            _provider.ChangePassword(user.UserName, oldPassword, newPassword);
        }

        public void ChangePasswordQuestionAndAnswer(MembershipUser user, string password, string question, string answer)
        {
            _provider.ChangePasswordQuestionAndAnswer(user.UserName, password, question, answer);
        }

        public string GetPassword(MembershipUser user, string passwordAnswer)
        {
            return _provider.GetPassword(user.UserName, passwordAnswer);
        }

        public string GetPassword(MembershipUser user)
        {
            return user.GetPassword();
        }

        public string ResetPassword(MembershipUser user, string passwordAnswer)
        {
            return _provider.ResetPassword(user.UserName, passwordAnswer);
        }

        public string ResetPassword(MembershipUser user)
        {
            return user.ResetPassword();
        }

        #endregion
    }
}