using System;
using FubuMVC.Core.View;
using Kokugen.Core.Validation;
using Kokugen.Web.Conventions;

namespace Kokugen.Web.Actions.Account.Manage.ChangePassword
{
    public class ChangePasswordAction
    {
        public ChangePasswordModel Query(ChangePasswordRequest request)
        {
            return new ChangePasswordModel();
        }

        public AjaxResponse Command(ChangePasswordModel model)
        {
            return new AjaxResponse();
        }
    }

    public class ChangePasswordRequest  : IRequestById
    {
        public Guid Id { get; set; }
    }

    public class ChangePasswordModel
    {
        public Guid Id { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePassword : FubuPage<ChangePasswordModel> { }
}