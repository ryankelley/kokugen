using Kokugen.Core.Domain;
using Kokugen.Core.Membership.Security;

namespace Kokugen.Core.Membership.Services
{
    public interface IMembershipValidator<USER> where USER : IUser
    {
        bool ValidateUser(USER userName, string password);
    }

    public interface IMembershipValidator : IMembershipValidator<User>{}
}