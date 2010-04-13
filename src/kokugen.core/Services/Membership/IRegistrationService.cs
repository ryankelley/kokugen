using System;
using FubuMVC.Core.Urls;
using Kokugen.Core.Domain;
using Kokugen.Core.Membership.Services;
using Kokugen.Core.Validation;

namespace Kokugen.Core.Services
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
        private readonly IUrlRegistry _urlRegistry;

        public RegistrationService(IUserService userService, IEmailService emailService, IUrlRegistry urlRegistry)
        {
            _userService = userService;
            _emailService = emailService;
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
                
        }

        private void SendConfirmationEmail(User user)
        {
            
        }

        #endregion
    }
}