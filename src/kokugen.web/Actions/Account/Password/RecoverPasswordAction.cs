using FubuMVC.Core;
using FubuMVC.Core.View;
using HtmlTags;
using Kokugen.Core.Membership.Settings;

namespace Kokugen.Web.Actions.Account.Password
{
    public class RecoverPasswordAction
    {
        private readonly PasswordResetRetrievalSettings _passwordResetRetrievalSettings;

        public RecoverPasswordAction(PasswordResetRetrievalSettings passwordResetRetrievalSettings)
        {
            _passwordResetRetrievalSettings = passwordResetRetrievalSettings;
        }

        [FubuPartial]
        public RecoverPasswordPartialModel Partial(RecoverPasswordPartialModel partialModel)
        {
            return new RecoverPasswordPartialModel(){Settings = _passwordResetRetrievalSettings};
        }

        public RecoverPasswordModel Query(RecoverPasswordRequest request)
        {
            return new RecoverPasswordModel(){Settings = _passwordResetRetrievalSettings};
        }

        public AjaxResponse Command(RecoverPasswordModel model)
        {
            return new AjaxResponse();
        }
    }

    public class RecoverPasswordRequest
    {
    }

    public class RecoverPasswordModel
    {
        public PasswordResetRetrievalSettings Settings { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }

    public class RecoverPasswordPartialModel
    {
        public PasswordResetRetrievalSettings Settings { get; set; }
    }

    public class PasswordPartial : FubuPage<RecoverPasswordPartialModel>{}

    public partial class PasswordRecoveryForm : FubuPage<RecoverPasswordModel>{}
  
}