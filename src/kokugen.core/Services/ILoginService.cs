using Kokugen.Core.Membership.Security;

namespace Kokugen.Core.Services
{
    public interface ILoginService
    {
        IUser LoginUser(string userName, string password, bool rememberMe);
        void Logout();
    }
}