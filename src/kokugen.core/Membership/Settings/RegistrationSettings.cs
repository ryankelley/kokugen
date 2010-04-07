namespace Kokugen.Core.Membership.Settings
{
    public class RegistrationSettings : IRegistrationSettings
    {

        #region IRegistrationSettings Members

        public bool RequiresUniqueEmailAddress { get; set; }

        #endregion
    }
}