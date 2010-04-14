using Kokugen.Core.Validation;

namespace Kokugen.Core.Membership.Security
{
    public interface IUser
    {
        string UserName { get; }
        object ProviderUserKey { get; }
        string Email { get; set; }
        bool IsLocked { get; }
        bool IsActivated { get; set; }
        string Password { get; }
        void Unlock();
    }
}