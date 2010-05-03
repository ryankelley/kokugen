using FubuMVC.Core;
using FubuMVC.Core.View;

namespace Kokugen.Web.Actions.Account.Login
{
    public class LoginFormAction
    {
        public LoginFormModel Query(LoginFormModel inModel)
        {
            return inModel;
        }
    }


    public class LoginFormModel : LoginModel
    {
        [QueryString]
        public string Message { get; set; }
    }

    public class LoginModel
    {
        [QueryString]
        public string ReturnUrl { get; set; }

        public virtual string Password { get; set; }
        public virtual string Login { get; set; }
        public virtual bool RememberMe { get; set; }
   
    }

    public class LoginForm : FubuPage<LoginFormModel>
    {
    }
}