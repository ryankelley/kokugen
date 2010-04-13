using FubuMVC.Core.View;
using Kokugen.Core.Domain;

namespace Kokugen.Web.Actions.Account.Register
{
    public class RegisterAccountAction
    {
        public RegisterAccountModel Query(RegisterAccountRequest request)
        {
            return new RegisterAccountModel();
        }

        public AjaxResponse Command(RegisterAccountModel inModel)
        {
            return new AjaxResponse();
        }

    }

    public class RegisterAccountRequest
    {
    }

    public class RegisterAccountModel
    {
        public User User { get; set; }
    }

    public class Register : FubuPage<RegisterAccountModel> { }
}