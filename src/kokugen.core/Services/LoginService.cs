#region

using System;
using System.Web.Security;
using FubuMVC.Core.Security;
using Kokugen.Core.Membership.Security;
using Kokugen.Core.Membership.Services;

#endregion

namespace Kokugen.Core.Services
{
    public class LoginService : ILoginService
    {
        private readonly IAuthenticationContext _authContext;
        private readonly IUserService _userService;
        private readonly IMembershipValidator _validator;

        public LoginService(IAuthenticationContext authContext, IUserService userService, IMembershipValidator validator)
        {
            _authContext = authContext;
            _userService = userService;
            _validator = validator;
        }

        public IUser LoginUser(string userName, string password, bool rememberMe)
        {
            if (_validator.ValidateUser(userName, password))
            {
                _authContext.ThisUserHasBeenAuthenticated(userName, rememberMe);
                return _userService.GetUserByLogin(userName);
            }
            return null;
        }

        public void Logout()
        {
            _authContext.SignOut();
        }
    }
}