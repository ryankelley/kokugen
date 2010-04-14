using System;
using FubuMVC.Core.View;
using Kokugen.Core.Membership.Services;
using Kokugen.Core.Validation;
using Kokugen.Web.Conventions;

namespace Kokugen.Web.Actions.Account.Manage.ChangePassword
{
    public class ChangePasswordAction
    {
        private readonly IPasswordService _passwordService;
        private readonly IUserService _userService;

        public ChangePasswordAction(IPasswordService passwordService, IUserService userService)
        {
            _passwordService = passwordService;
            _userService = userService;
        }

        public ChangePasswordModel Query(ChangePasswordRequest request)
        {
            return new ChangePasswordModel(){Id = request.Id};
        }

        public AjaxResponse Command(ChangePasswordModel model)
        {
            if(model.NewPassword != model.ConfirmPassword)
                return new AjaxResponse(){Item = "New password doesn't match confirm password"};
            try
            {
                _passwordService.ChangePassword(_userService.Retrieve(model.Id), model.OldPassword, model.NewPassword);
                return new AjaxResponse(){Success = true, Item = "Password successfully changed"};
            }
            catch(Exception ex)
            {
                return new AjaxResponse(){Item = ex.Message};
            }
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