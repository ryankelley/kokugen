using System;
using System.Collections.Generic;
using System.Web.Security;
using Kokugen.Core.Membership.Services;
using Kokugen.Core.Membership.Settings;
using Kokugen.Core.Validation;

namespace Kokugen.Core.Membership.Abstractions
{
    public class AspNetMembershipProviderWrapper : IUserService, IMembershipValidator
    {
        private readonly MembershipProvider _provider;
        private readonly MembershipSettingsBag _settingsBag;

        public AspNetMembershipProviderWrapper(MembershipProvider provider, 
            MembershipSettingsBag settingsBag)
        {
            _provider = provider;
            _settingsBag = settingsBag;
        }

        public MembershipCreateStatus Create(MembershipUser user)
        {
            throw new NotImplementedException();
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
    }
}