namespace Kokugen.Core.Membership.Settings
{
    public class LoginSettings
    {
        public int MaxInvalidPasswordAttempts { get;  set; }
        public int PasswordAttemptWindow { get;  set; }
    }
}