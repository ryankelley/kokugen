namespace Kokugen.Core.Membership.Settings
{
    public class RegistrationSettings 
    {
        public RegistrationSettings()
        {
            RequiresUniqueEmail = true;
        }

        public bool RequiresUniqueEmail { get; set; }
    }
}