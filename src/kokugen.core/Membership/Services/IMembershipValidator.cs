using Kokugen.Core.Membership.Security;

namespace Kokugen.Core.Membership.Services
{
    public interface IMembershipValidator
    {
        bool ValidateUser(IUser userName, string password);
        bool ValidateUser(string name, string password);
    }
}