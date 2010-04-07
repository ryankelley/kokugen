namespace Kokugen.Core.Membership.Settings
{
    public interface IRegistrationSettings
    {
        bool RequiresUniqueEmailAddress{ get; }
    }
}