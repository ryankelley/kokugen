using FubuMVC.Core.Continuations;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.Account.Login
{
    public class LoginAction
    {
        private readonly ILoginService _loginService;

        public LoginAction(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public FubuContinuation Command(LoginModel inModel)
        {
            var user = _loginService.LoginUser(inModel.Login, inModel.Password, inModel.RememberMe);

            //redirect here instead
            return user != null
                       ? FubuContinuation.RedirectTo(inModel.ReturnUrl)
                       : FubuContinuation.RedirectTo(new LoginFormModel()
                                                         {Message = "User name or password was incorrect.",
                                                         ReturnUrl = inModel.ReturnUrl});
        }

        
    }
}