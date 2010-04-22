using System.Linq;
using FubuMVC.Core.View;
using Kokugen.Core.Attributes;
using Kokugen.Core.Domain;
using Kokugen.Core.Membership.Services;
using Kokugen.Core.Membership.Settings;
using Kokugen.Core.Validation;

namespace Kokugen.Web.Actions.Account.Register
{
    public class RegisterAccountAction
    {
        private readonly IRegistrationService _registrationService;
        private readonly PasswordResetRetrievalSettings _passwordResetRetrievalSettings;

        public RegisterAccountAction(IRegistrationService registrationService,
            PasswordResetRetrievalSettings passwordResetRetrievalSettings)
        {
            _registrationService = registrationService;
            _passwordResetRetrievalSettings = passwordResetRetrievalSettings;
        }

        public RegisterAccountModel Query(RegisterAccountRequest request)
        {
            return new RegisterAccountModel(){Settings = _passwordResetRetrievalSettings};
        }

        public AjaxResponse Command(RegisterAccountModel inModel)
        {
            var user = new User(inModel.User.UserName, inModel.User.Email, inModel.User.Password)
                           {
                               FirstName = inModel.User.FirstName,
                               LastName = inModel.User.LastName,
                               Question = inModel.User.Question,
                               Answer = inModel.User.Answer
                           };

            var notification = _registrationService.RegisterUser(user);

            return notification.IsValid()
                       ? new AjaxResponse()
                             {
                                 Success = true,
                                 Item =
                                     "Thank you for registering! You will be recieving an email to confirm your account shortly"
                             }
                       : new AjaxResponse() { Item = notification.AllMessages.Select(x => x.FieldName + ": " + x.Message).Aggregate((x, y) => x + " /n" + y) };
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
        [ValueOf("Question"), Required]
        public string Question { get; set; }
        [Required]
        public string Answer { get; set; }
    }

    public class RegisterAccountModel
    {
        public PasswordResetRetrievalSettings Settings { get; set; }

        public UserDTO User { get; set; }
    }

    public class Register : FubuPage<RegisterAccountModel> { }
}