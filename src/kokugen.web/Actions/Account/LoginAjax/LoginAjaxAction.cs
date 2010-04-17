using FubuCore;
using FubuMVC.Core.Urls;
using Kokugen.Core.Membership.Services;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.Account.LoginAjax
{
    public class LoginAjaxAction
    {
        private readonly ILoginService _loginService;
        private readonly IUserService _userService;

        public LoginAjaxAction(ILoginService loginService, IUserService userService)
        {
            _loginService = loginService;
            _userService = userService;
        }

        public AjaxResponse Command(AjaxLoginModel inModel)
        {
            if (inModel.Login.IsNotEmpty() && inModel.Password.IsNotEmpty())
            {
                var userValid = _loginService.LoginUser(inModel.Login, inModel.Password, true);
                var user = _userService.GetUserByLogin(inModel.Login);

                if (userValid.IsValid())
                {
                    return new AjaxResponse {Success = true, Item = user.Id};
                }
            }
            return new AjaxResponse {Success = false, Item = "Login failed"};

        }
    }

    public class AjaxLoginModel
    {
        public virtual string Password { get; set; }
        public virtual string Login { get; set; }
        
    }
}