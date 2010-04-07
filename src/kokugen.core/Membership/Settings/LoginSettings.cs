namespace Kokugen.Core.Membership.Settings
{
    public class LoginSettings
    {
        public int MaxInvalidPasswordAttempts { get; private set; }
        public int PasswordAttemptWindow { get; private set; }
    }
}