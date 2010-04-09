using System.Web.Security;

namespace Kokugen.Core.Membership.Settings
{
    public class PasswordSettings
    {
        public int MinRequiredPasswordLength { get; private set; }
        public int MinRequiredNonAlphanumericCharacters { get; private set; }
        public string PasswordStrengthRegularExpression { get; private set; }
        public MembershipPasswordFormat PasswordFormat { get; private set; }

    }
}