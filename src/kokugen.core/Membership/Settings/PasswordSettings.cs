using System;
using System.Web.Security;

namespace Kokugen.Core.Membership.Settings
{
    public class PasswordSettings
    {
        public PasswordSettings()
        {
            MinRequiredPasswordLength = 8;
            MinRequiredNonAlphanumericCharacters = 1;
            PasswordFormat = PasswordFormat.Hashed;
            PasswordStrengthRegularExpression = "";
        }

        private int _minRequiredPasswordLength;
        public int MinRequiredPasswordLength
        {
            get { return _minRequiredPasswordLength; }
            set
            {
                if(value < 6)
                    throw new InvalidOperationException("Password length must be at least 6, come on man, seriously!");
                _minRequiredPasswordLength = value;
            }
        }

        public int MinRequiredNonAlphanumericCharacters { get; set; }
        public string PasswordStrengthRegularExpression { get; set; }
        public PasswordFormat PasswordFormat { get; set; }

        public string GetValidationMessage()
        {
            return
                string.Format(
                    "Your password must be at least {0} characters long with at least {1} non alpha-numberic characters",
                    MinRequiredPasswordLength, MinRequiredNonAlphanumericCharacters);
        }
    }

    public enum PasswordFormat
    {
        Hashed,
        Clear,
        Encrypted
    }
}