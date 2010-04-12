namespace Kokugen.Core.Membership.Settings
{
    public class LoginSettings
    {
        public LoginSettings()
        {
            MaxInvalidPasswordAttempts = 5;
            PasswordAttemptWindow = 10;
        }

        public int MaxInvalidPasswordAttempts { get; private set; }
        public int PasswordAttemptWindow { get; private set; }
    }
}