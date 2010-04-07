namespace Kokugen.Core.Membership.Settings
{
    public interface ILoginSettings
    {
        int MaximumInvalidPasswordAttempts { get; }
        int PasswordAttemptWindowInMinutes { get; }
    }
}