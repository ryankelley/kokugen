using Kokugen.Core.Membership.Services;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.Login
{
    public class LoginAction
    {
        private readonly ILoginService _loginService;

        public LoginAction(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public AjaxResponse Command(LoginModel inModel)
        {
            var user = _loginService.LoginUser(inModel.Login, inModel.Password, inModel.RememberMe);

            return user != null
                       ? new AjaxResponse() { Success = true, Item = user}
                       : new AjaxResponse() { Success = false };
        }

        
    }
}