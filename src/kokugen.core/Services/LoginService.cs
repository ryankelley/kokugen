#region

using System;
using System.Web.Security;
using FubuMVC.Core.Security;
using Kokugen.Core.Domain;
using Kokugen.Core.Membership.Security;
using Kokugen.Core.Membership.Services;
using Kokugen.Core.Validation;

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

        public INotification LoginUser(string userName, string password, bool rememberMe)
        {
            var notification = new Notification();

            var login = _userService.GetUserByLogin(userName);
            if (login == null)
            {
                notification.RegisterMessage("userName", "User name not found!", Severity.Error);
                return notification;
            }

            if(!login.IsActivated && login.UserName != "KokugenAdmin")
            {
                notification.RegisterMessage("user", "Your account has not been activated yet!", Severity.Error);
                return notification;
            }

            if(login.IsLocked)
            {
                notification.RegisterMessage("userName", "Your account has not been locked!", Severity.Error);
                return notification;
            }


            if (_validator.ValidateUser(login, password))
            {
                _authContext.ThisUserHasBeenAuthenticated(userName, rememberMe);
                return notification;
            }

            notification.RegisterMessage("password", "Password was incorrect!", Severity.Error);
            return notification;
        }

        public void Logout()
        {
            _authContext.SignOut();
        }
    }
}