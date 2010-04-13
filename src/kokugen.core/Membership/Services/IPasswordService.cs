#region

using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using Kokugen.Core.Domain;
using Kokugen.Core.Membership.Security;

#endregion

namespace Kokugen.Core.Membership.Services
{
    public interface IPasswordService<USER> where USER : IUser
    {
        void Unlock(USER user);
        void ChangePassword(USER user, string oldPassword, string newPassword);
        void ChangePasswordQuestionAndAnswer(USER user, string password, string question, string answer);

        string GetPassword(USER user, string passwordAnswer);
        string GetPassword(USER user);

        string ResetPassword(USER user, string passwordAnswer);
        string ResetPassword(USER user);
    }

    public interface IPasswordService : IPasswordService<User>{}
   
}