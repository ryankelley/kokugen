using System;
using Kokugen.Core.Membership.Security;
using Kokugen.Core.Membership.Services;

namespace Kokugen.Core.Services
{
    public class PasswordService : IPasswordService
    {
        public void Unlock(IUser user)
        {
            throw new NotImplementedException();
        }

        public void ChangePassword(IUser user, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public void ChangePasswordQuestionAndAnswer(IUser user, string password, string question, string answer)
        {
            throw new NotImplementedException();
        }

        public string GetPassword(IUser user, string passwordAnswer)
        {
            throw new NotImplementedException();
        }

        public string GetPassword(IUser user)
        {
            throw new NotImplementedException();
        }

        public string ResetPassword(IUser user, string passwordAnswer)
        {
            throw new NotImplementedException();
        }

        public string ResetPassword(IUser user)
        {
            throw new NotImplementedException();
        }
    }
}