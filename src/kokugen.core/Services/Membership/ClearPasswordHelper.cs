using System;

namespace Kokugen.Core.Services
{
    public class ClearPasswordHelper : IPasswordHelperService
    {
        #region Implementation of IPasswordHelperService

        public string CreatePasswordHash(string password)
        {
            return password;
        }

        public bool ComparePasswordToHash(string password, string passwordHash)
        {
            return password == passwordHash;
        }

        public string RandomPasswordNoHash(int length)
        {
            throw new NotImplementedException();
        }

        public string RandomPasswordNoHash(int length, int nonAlphaNumberic)
        {
            throw new NotImplementedException();
        }

        public string RandomPasswordHashed(int length)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}