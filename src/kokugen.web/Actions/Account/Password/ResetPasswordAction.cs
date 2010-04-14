using System;
using FubuMVC.Core;
using FubuMVC.Core.Security;
using FubuMVC.Core.View;
using HtmlTags;
using Kokugen.Core.Membership.Services;
using Kokugen.Core.Membership.Settings;
using Kokugen.Core.Validation;

namespace Kokugen.Web.Actions.Account.Password
{
    public class ResetPasswordAction
    {
        private readonly IUserService _userService;
        private readonly IPasswordService _passwordService;
        private readonly ISecurityContext _securityContext;
        private readonly PasswordResetRetrievalSettings _passwordResetRetrievalSettings;

        public ResetPasswordAction(IUserService userService, IPasswordService passwordService,
            PasswordResetRetrievalSettings passwordResetRetrievalSettings)
        {
            _userService = userService;
            _passwordService = passwordService;
            _passwordResetRetrievalSettings = passwordResetRetrievalSettings;
        }

        [FubuPartial]
        public ResetPasswordPartialModel Partial(ResetPasswordPartialModel partialModel)
        {
            return new ResetPasswordPartialModel(){Settings = _passwordResetRetrievalSettings};
        }

        public ResetPasswordModel Query(ResetPasswordRequest request)
        {
            var returnModel = new ResetPasswordModel(){Settings = _passwordResetRetrievalSettings, Email = request.Email};

            if(_passwordResetRetrievalSettings.RequiresQuestionAndAnswer)
            {
                var userByEmail = _userService.GetUserByEmail(request.Email);
                if (userByEmail != null) returnModel.Question = userByEmail.Question;
            }

            return returnModel;

        }

        public AjaxResponse Command(ResetPasswordModel model)
        {

            var user = _userService.GetUserByEmail(model.Email);
            if(user == null)
                return new AjaxResponse(){Item = "Could not find your email in our system"};
            try
            {

                if (_passwordResetRetrievalSettings.RequiresQuestionAndAnswer)
                    _passwordService.ResetPassword(user, model.Answer);
                else
                {
                    _passwordService.ResetPassword(user);
                }
            }
            catch(Exception ex)
            {
                return new AjaxResponse(){Item = ex.Message};
            }

            return new AjaxResponse(){Success = true, Item = "An new password has been sent to your email account"};
        }
    }

    public class ResetPasswordRequest
    {
        [QueryString]
        public string Email { get; set; }
    }

    public class ResetPasswordModel
    {
        public string Email { get; set; }
        public PasswordResetRetrievalSettings Settings { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }

    public class ResetPasswordPartialModel
    {
        public string Email { get; set; }
        public PasswordResetRetrievalSettings Settings { get; set; }
    }

    public class PasswordPartial : FubuPage<ResetPasswordPartialModel>{}

    public partial class PasswordResetForm : FubuPage<ResetPasswordModel>{}
  
}