
namespace Kokugen.Core.Membership.Settings
{
    public class MembershipSettingsBag
    {
        public MembershipSettingsBag(LoginSettings login, PasswordSettings password, RegistrationSettings registration, PasswordResetRetrievalSettings passwordResetRetrievalSettings)
        {
            Login = login;
            Password = password;
            Registration = registration;
            PasswordResetRetrievalSettings = passwordResetRetrievalSettings;
        }

        public LoginSettings Login { get; set; }
        public PasswordSettings Password { get; set; }
        public RegistrationSettings Registration { get; set; }
        public PasswordResetRetrievalSettings PasswordResetRetrievalSettings { get; set; }
    }
}