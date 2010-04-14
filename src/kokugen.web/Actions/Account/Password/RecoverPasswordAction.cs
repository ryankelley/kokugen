using FubuMVC.Core;
using FubuMVC.Core.Security;
using FubuMVC.Core.View;
using HtmlTags;
using Kokugen.Core.Membership.Services;
using Kokugen.Core.Membership.Settings;

namespace Kokugen.Web.Actions.Account.Password
{
    public class RecoverPasswordAction
    {
        private readonly IUserService _userService;
        private readonly ISecurityContext _securityContext;
        private readonly PasswordResetRetrievalSettings _passwordResetRetrievalSettings;

        public RecoverPasswordAction(IUserService userService, ISecurityContext securityContext,
            PasswordResetRetrievalSettings passwordResetRetrievalSettings)
        {
            _userService = userService;
            _securityContext = securityContext;
            _passwordResetRetrievalSettings = passwordResetRetrievalSettings;
        }

        [FubuPartial]
        public RecoverPasswordPartialModel Partial(RecoverPasswordPartialModel partialModel)
        {
            return new RecoverPasswordPartialModel(){Settings = _passwordResetRetrievalSettings};
        }

        public RecoverPasswordModel Query(RecoverPasswordRequest request)
        {
            var returnModel = new RecoverPasswordModel(){Settings = _passwordResetRetrievalSettings};

            if(_passwordResetRetrievalSettings.RequiresQuestionAndAnswer)
            {
                var userByEmail = _userService.GetUserByEmail(request.Email);
                if (userByEmail != null) returnModel.Question = userByEmail.Question;
            }

            return returnModel;

        }

        public AjaxResponse Command(RecoverPasswordModel model)
        {
            return new AjaxResponse();
        }
    }

    public class RecoverPasswordRequest
    {
        [QueryString]
        public string Email { get; set; }
    }

    public class RecoverPasswordModel
    {
        public PasswordResetRetrievalSettings Settings { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }

    public class RecoverPasswordPartialModel
    {
        public string Email { get; set; }
        public PasswordResetRetrievalSettings Settings { get; set; }
    }

    public class PasswordPartial : FubuPage<RecoverPasswordPartialModel>{}

    public partial class PasswordRecoveryForm : FubuPage<RecoverPasswordModel>{}
  
}