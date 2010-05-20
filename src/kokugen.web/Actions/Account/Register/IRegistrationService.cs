using System;
using FubuCore;
using FubuMVC.Core.Urls;
using HtmlTags;
using Kokugen.Core.Domain;
using Kokugen.Core.Membership.Services;
using Kokugen.Core.Services;
using Kokugen.Core.Validation;
using Kokugen.Web.Actions.Account.Activate;

namespace Kokugen.Web.Actions.Account.Register
{
    public interface IRegistrationService
    {
        INotification RegisterUser(User user);
        void ActivateAccount(Guid id);
    }

    public class RegistrationService  : IRegistrationService
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly EmailSettings _emailSettings;
        private readonly IUrlRegistry _urlRegistry;

        public RegistrationService(IUserService userService, 
            IEmailService emailService, 
            EmailSettings emailSettings,
            IUrlRegistry urlRegistry)
        {
            _userService = userService;
            _emailService = emailService;
            _emailSettings = emailSettings;
            _urlRegistry = urlRegistry;
        }

        #region Implementation of IRegistrationService

        public INotification RegisterUser(User user)
        {
            var notification = _userService.Create(user);

            if (notification.IsValid())
                SendConfirmationEmail(user);

            return notification;

        }

        public void ActivateAccount(Guid id)
        {
            var user = _userService.Retrieve(id);
            user.IsActivated = true;
            _userService.Update(user);
        }

        private void SendConfirmationEmail(User user)
        {
            
            var email = new HtmlTag("body")
                .Child(new HtmlTag("h3").Text("Thank you for registering"))
                .Child(new HtmlTag("div").Id("wrapper")
                           .Child(new LinkTag("Click here to activate your account",
                                              UrlContext.ToFull(_urlRegistry.UrlFor(new ActivateAccountModel() {Id = user.Id})))));
            _emailService.SendEmail(user.Email, _emailSettings.DefaultFromEmailAddress, "Activate your account", email.ToString());
        }

        #endregion
    }
}