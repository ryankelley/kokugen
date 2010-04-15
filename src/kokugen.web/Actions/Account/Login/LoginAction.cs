using System.Linq;
using FubuMVC.Core.Continuations;
using FubuMVC.Core.Urls;
using Kokugen.Core.Services;
using Kokugen.Web.Actions.Home;

namespace Kokugen.Web.Actions.Account.Login
{
    public class LoginAction
    {
        private readonly ILoginService _loginService;
        private readonly IUrlRegistry _urlRegistry;

        public LoginAction(ILoginService loginService, IUrlRegistry urlRegistry)
        {
            _loginService = loginService;
            _urlRegistry = urlRegistry;
        }

        public FubuContinuation Command(LoginModel inModel)
        {
            var user = _loginService.LoginUser(inModel.Login, inModel.Password, inModel.RememberMe);

            //redirect here instead
            return user.IsValid()
                       ? inModel.ReturnUrl != ""
                             ? FubuContinuation.RedirectTo(inModel.ReturnUrl)
                             : FubuContinuation.RedirectTo(_urlRegistry.UrlFor<IndexAction>(x => x.Query()))
                       : FubuContinuation.RedirectTo(new LoginFormModel()
                                                         {
                                                             Message = user.AllMessages.First().Message,
                                                             ReturnUrl = inModel.ReturnUrl
                                                         });
        }


    }
}