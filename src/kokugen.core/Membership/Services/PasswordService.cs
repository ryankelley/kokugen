#region

using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using Kokugen.Core.Membership.Security;

#endregion

namespace Kokugen.Core.Membership.Services
{
    public interface IPasswordService
    {
        void Unlock(IUser user);
        void ChangePassword(IUser user, string oldPassword, string newPassword);
        void ChangePasswordQuestionAndAnswer(IUser user, string password, string question, string answer);

        string GetPassword(IUser user, string passwordAnswer);
        string GetPassword(IUser user);

        string ResetPassword(IUser user, string passwordAnswer);
        string ResetPassword(IUser user);
    }

   
}