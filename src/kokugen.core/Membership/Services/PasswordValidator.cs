using System.Text.RegularExpressions;
using Kokugen.Core.Membership.Services;
using Kokugen.Core.Membership.Settings;

namespace Kokugen.Tests.Core.Membership.Services
{
    public class PasswordValidator : IPasswordValidator 
    {
        private readonly PasswordSettings _passwordSettings;

        public PasswordValidator(PasswordSettings passwordSettings)
        {
            _passwordSettings = passwordSettings;
        }

        public bool ValidatePassword(string password)
        {
            var valid = true;

            valid = ValidatePasswordLength(password);

            if (valid)
                valid = ValidateNonAlphaContraint(password);

            if (valid)
                valid = ValidateCustomRegex(password);

            return valid; 
        }

        private bool ValidateCustomRegex(string password)
        {
            return new Regex(_passwordSettings.PasswordStrengthRegularExpression).IsMatch(password);
        }

        private bool ValidatePasswordLength(string password)
        {
            var pattern = "^(?=.{" + _passwordSettings.MinRequiredPasswordLength + ",})";

            var regex = new Regex(pattern);

            return regex.IsMatch(password);
        }

        private bool ValidateNonAlphaContraint(string password)
        {
            var pattern = _passwordSettings.MinRequiredNonAlphanumericCharacters > 0
                              ? "(?=(.*\\W.*){" + _passwordSettings.MinRequiredNonAlphanumericCharacters + ",})"
                              : "";
          
            
            var regex = new Regex(pattern);

            return regex.IsMatch(password);
        }
    }
}