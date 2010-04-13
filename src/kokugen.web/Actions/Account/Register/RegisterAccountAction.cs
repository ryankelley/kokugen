using FubuMVC.Core.View;
using Kokugen.Core.Domain;
using Kokugen.Core.Membership.Services;
using Kokugen.Core.Validation;

namespace Kokugen.Web.Actions.Account.Register
{
    public class RegisterAccountAction
    {
        private readonly IRegistrationService _registrationService;

        public RegisterAccountAction(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        public RegisterAccountModel Query(RegisterAccountRequest request)
        {
            return new RegisterAccountModel();
        }

        public AjaxResponse Command(RegisterAccountModel inModel)
        {
            var user = new User(inModel.User.UserName, inModel.User.Email, inModel.User.Password)
                           {
                               FirstName = inModel.User.FirstName,
                               LastName = inModel.User.LastName
                           };

            var notification = _registrationService.RegisterUser(user);

            return notification.IsValid()
                       ? new AjaxResponse()
                             {
                                 Success = true,
                                 Item =
                                     "Thank you for registering! You will be recieving an email to confirm your account shortly"
                             }
                       : new AjaxResponse() {Item = new {messages = notification.AllMessages}};
        }

    }

    public class RegisterAccountRequest
    {
    }

    public class UserDTO
    {
        [Required]
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required,ValidEmail]
        public string Email { get; set; }
    }

    public class RegisterAccountModel
    {
        public UserDTO User { get; set; }
    }

    public class Register : FubuPage<RegisterAccountModel> { }
}