#region

using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

#endregion

namespace Kokugen.Core.Membership.Services
{
    public interface IPasswordService
    {
        void Unlock(MembershipUser user);
        void ChangePassword(MembershipUser user, string oldPassword, string newPassword);
        void ChangePasswordQuestionAndAnswer(MembershipUser user, string password, string question, string answer);
       
        string GetPassword(MembershipUser user, string passwordAnswer);
        string GetPassword(MembershipUser user);

        string ResetPassword(MembershipUser user, string passwordAnswer);
        string ResetPassword(MembershipUser user);
    }

   
}