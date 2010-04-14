using System;
using HtmlTags;
using Kokugen.Core.Domain;
using Kokugen.Core.Membership.Security;
using Kokugen.Core.Membership.Services;
using Kokugen.Core.Membership.Settings;
using Kokugen.Core.Persistence.Repositories;
using Kokugen.Core.Validation;

namespace Kokugen.Core.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly IPasswordHelperService _passwordHelper;
        private readonly IUserRepository _userRepository;
        private readonly IValidator _validator;
        private readonly IEmailService _emailService;
        private readonly MembershipSettingsBag _settings;

        public PasswordService(IPasswordHelperService passwordHelper,
            IUserRepository userRepository, 
            IValidator validator,
            IEmailService emailService,
            MembershipSettingsBag settings)
        {
            _passwordHelper = passwordHelper;
            _userRepository = userRepository;
            _validator = validator;
            _emailService = emailService;
            _settings = settings;
        }

        public void Unlock(User entity)
        {
            entity.Unlock();

            ValidateAndSave(entity);
        }

        private void ValidateAndSave(User entity)
        {
            var results = _validator.Validate(entity);
            if (results.IsValid())
                _userRepository.Save(entity);
        }

        public void ChangePassword(User user, string oldPassword, string newPassword)
        {
            if(_settings.PasswordResetRetrievalSettings.RequiresQuestionAndAnswer)
                throw new InvalidOperationException("Password requires question and answer to change");

            var entity = user as User;
            if (entity == null) return;

            if(!_passwordHelper.ComparePasswordToHash(oldPassword, entity.Password))
                throw new InvalidOperationException("The old password provided does not match the current password.");

            switch (_settings.Password.PasswordFormat)
            {
                case PasswordFormat.Hashed:
                    entity.ChangePassword(_passwordHelper, newPassword);
                    break;
                case PasswordFormat.Clear:
                    entity.ChangePassword(new ClearPasswordHelper(), newPassword);
                    break;
                case PasswordFormat.Encrypted:
                    throw new NotImplementedException();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            ValidateAndSave(entity);
            
        }

        public void ChangePasswordQuestionAndAnswer(User user, string password, string question, string answer)
        {
            throw new NotImplementedException();
        }

        public string GetPassword(User user, string passwordAnswer)
        {
            throw new NotImplementedException();
        }

        public string GetPassword(User user)
        {
            if(_settings.PasswordResetRetrievalSettings.RequiresQuestionAndAnswer)
                throw new InvalidOperationException("Password requires question and answer to retrieve");

            var entity = user as User;

            switch (_settings.Password.PasswordFormat)
            {
                case PasswordFormat.Hashed:
                    throw new InvalidOperationException("Hashed passwords cannot be retieved");
                    break;
                case PasswordFormat.Clear:
                    return entity.Password;
                    break;
                case PasswordFormat.Encrypted:
                    throw new NotImplementedException();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void ResetPassword(User user, string passwordAnswer)
        {
            if (!_settings.PasswordResetRetrievalSettings.EnablePasswordReset)
                throw new InvalidOperationException("Password reset is not enabled");
            
            
            if(_settings.PasswordResetRetrievalSettings.RequiresQuestionAndAnswer)
            {
               if(user.Answer != passwordAnswer)
                   throw new InvalidOperationException("Password answer does not match");
            }

            var newPassword = resetPassword(user);

            emailUserNewPassword(newPassword, user);

        }

        private void emailUserNewPassword(string newPassword, User user)
        {
            var email = new HtmlTag("div")
                .Child(new HtmlTag("h3", tag =>
                               {
                                   tag.Text("Your password has been reset");
                               }))
                .Child(new HtmlTag("span", tag => tag.Text("Your new password is: ")))
                .Child(new HtmlTag("span", tag => tag.Text(newPassword)));

            _emailService.SendEmail(user.Email, "no-reply@kokugen.com","Your Password Has Been Reset", email.ToString());
        }

        public void ResetPassword(User user)
        {
            
            if (!_settings.PasswordResetRetrievalSettings.EnablePasswordReset)
                throw new InvalidOperationException("Password reset is not enabled");


            var newPassword = resetPassword(user);

            emailUserNewPassword(newPassword, user);
        }

        private string resetPassword(User entity)
        {
            var newPassword = _passwordHelper.RandomPasswordNoHash(_settings.Password.MinRequiredPasswordLength,
                                                                   _settings.Password.
                                                                       MinRequiredNonAlphanumericCharacters);

            switch (_settings.Password.PasswordFormat)
            {
                case PasswordFormat.Hashed:
                    entity.ChangePassword(_passwordHelper, newPassword);
                    break;
                case PasswordFormat.Clear:
                    entity.ChangePassword(new ClearPasswordHelper(), newPassword);
                    break;
                case PasswordFormat.Encrypted:
                    throw new NotImplementedException();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            ValidateAndSave(entity);

            return newPassword;
        }
    }
}