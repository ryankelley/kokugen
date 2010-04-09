using Kokugen.Core.Membership.Services;

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
            var user = _loginService.AuthenticateUser(inModel.Login, inModel.Password);

            return user != null
                       ? new AjaxResponse() { Success = true, Item = user}
                       : new AjaxResponse() { Success = false };
        }

        
    }
}