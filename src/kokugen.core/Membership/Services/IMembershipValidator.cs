namespace Kokugen.Core.Membership.Services
{
    public interface IMembershipValidator
    {
        bool ValidateUser(string userName, string password);
    }
}