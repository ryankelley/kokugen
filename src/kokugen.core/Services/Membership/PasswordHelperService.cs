using System;
using System.Text;

namespace Kokugen.Core.Services
{
    public class PasswordHelperService : IPasswordHelperService
    {
        #region IPasswordHelperService Members

        public string CreatePasswordHash(string password)
        {
            return SimpleHash.ComputeHash(password, "MD5", null);
        }

        public bool ComparePasswordToHash(string password, string passwordHash)
        {
            return SimpleHash.VerifyHash(password, "MD5", passwordHash);
        }

        public string RandomPasswordNoHash(int length)
        {
            if (length <= 0)
            {
                length = 8;
            }

            var random = new Random();
            String characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var newString = new StringBuilder();
            while (length-- > 0)
            {
                newString.Append(characters[(int)(random.NextDouble() * characters.Length)]);
            }

            return newString.ToString();
        }

        public string RandomPasswordNoHash(int length, int nonAlphaNumberic)
        {
            
            var newString = new StringBuilder();
            newString.Append(RandomPasswordNoHash(length));

            var random = new Random();
            String characters = "~!@#$%^&*()_+";
            while (nonAlphaNumberic-- > 0)
            {
                newString.Append(characters[(int)(random.NextDouble() * characters.Length)]);
            }

            return newString.ToString();
        }

        public string RandomPasswordHashed(int length)
        {
            return CreatePasswordHash(RandomPasswordNoHash(length));
        }

        #endregion
    }
}