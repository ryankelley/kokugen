#region

using FubuMVC.Core.Security;
using Kokugen.Core.Domain;

#endregion

namespace Kokugen.Core.Membership.Services
{
    public interface ILoginService
    {
        User AuthenticateUser(string userName, string password);
    }

    public class LoginService : ILoginService
    {
        private readonly IAuthenticationContext _authContext;
        private readonly IUserService _userService;

        private readonly IPasswordHelperService _passService;

        public LoginService(IAuthenticationContext authContext, IUserService userService,
                            IPasswordHelperService passService)
        {
            _authContext = authContext;
            _userService = userService;
            _passService = passService;
        }

        public User AuthenticateUser(string userName, string password)
        {
            var user = _userService.GetUserByLogin(userName);
            
            if(user != null && _passService.ComparePasswordToHash(password, user.HashedPassword))
            {
                _authContext.ThisUserHasBeenAuthenticated(user.Id.ToString(),false);
                return user;
            }
            return null;
            
        }
    }
}