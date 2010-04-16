using Kokugen.Core.Membership.Security;
using Kokugen.Core.Validation;

namespace Kokugen.Core.Services
{
    public interface ILoginService
    {
        INotification LoginUser(string userName, string password, bool rememberMe);
        void Logout();
    }
}