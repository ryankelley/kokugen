using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using Kokugen.Core.Domain;
using Kokugen.Core.Membership.Security;
using Kokugen.Core.Membership.Services;
using Kokugen.Core.Membership.Settings;
using Kokugen.Core.Persistence.Repositories;
using Kokugen.Core.Validation;
using PagedList;

namespace Kokugen.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHelperService _passwordHelperService;
        private readonly IPasswordValidator _passwordValidator;
        private readonly IValidator _validator;
        private readonly MembershipSettingsBag _settings;

        public UserService(IUserRepository userRepository, 
            IPasswordHelperService passwordHelperService,
            IPasswordValidator passwordValidator,
            IValidator validator,
            MembershipSettingsBag settings)
        {
            _userRepository = userRepository;
            _passwordHelperService = passwordHelperService;
            _passwordValidator = passwordValidator;
            _validator = validator;
            _settings = settings;
        }

        public INotification Update(User user)
        {
            return ValidateAndUpdate(user);
        }

        private INotification ValidateAndUpdate(User entity)
        {
            var notification = _validator.Validate(entity);
            if (notification.IsValid())
            {
                //make sure email is unique
                var user = _userRepository.FindBy(x => x.Email, entity.Email);
                if (user != null)
                {
                    if (user.Id != entity.Id)
                    {
                        notification.RegisterMessage("Email", "Email already exists!", Severity.Error);
                        _userRepository.Evict(entity);
                        return notification;
                    }
                }

                _userRepository.Save(entity);
            }
            else
            {
                _userRepository.Evict(entity);
            }
            return notification;
        }

        public void Delete(User user)
        {
            var entity = user as Domain.User;
            if (entity != null) _userRepository.Delete(entity);
        }

        public User Retrieve(object id)
        {
            return _userRepository.Get((Guid) id);
        }

        public User GetUserByLogin(string name)
        {
            return _userRepository.FindBy(x => x.UserName, name);
        }

        public User GetUserByEmail(string email)
        {
            if(!_settings.Registration.RequiresUniqueEmail)
                throw new InvalidOperationException("RegistrationSettings.RequireUniqueEmail must be true to retrieve users by email");

            return _userRepository.FindBy(x => x.Email, email);
        }

        public IEnumerable<User> FindAll()
        {
            return _userRepository.Query().Where(x => x.IsActivated).OrderBy(x => x.LastName).ToList();
        }

        public User GetUserById(Guid userId)
        {
            return _userRepository.Get(userId);
        }

        public IPagedList<User> FindAll(int pageIndex, int pageSize)
        {
            var users = _userRepository.Query()
                .Where(x => x.IsActivated)
                .Take(pageSize)
                .Skip(pageIndex*pageSize);
            return new StaticPagedList<User>(users, pageIndex, pageSize, TotalUsers);
        }

        public int TotalUsers
        {
            get { return _userRepository.Query().Count(); }
        }


        public INotification Create(User user)
        {
            user.GravatarHash = user.Email.ToGravatarHash();

            return ValidateAndCreate(user);
        }

        private INotification ValidateAndCreate(Domain.User user)
        {
            var notification = _validator.Validate(user);

            if (notification.IsValid())
            {
                //verify valid password
                if (!_passwordValidator.ValidatePassword(user.Password))
                {
                    notification.RegisterMessage("Password", _settings.Password.GetValidationMessage(), Severity.Error);
                    return notification;
                }

                //make sure user is unique
                if(_userRepository.FindBy(x => x.UserName, user.UserName) != null)
                {
                    notification.RegisterMessage("UserName", "User name already exists!", Severity.Error);
                    return notification;
                }

                //make sure email is unique
                if (_userRepository.FindBy(x => x.Email, user.Email) != null)
                {
                    notification.RegisterMessage("Email", "Email already exists!", Severity.Error);
                    return notification;
                }

                switch (_settings.Password.PasswordFormat)
                {
                    case PasswordFormat.Hashed:
                        user.SetPassword(_passwordHelperService);
                        break;
                    case PasswordFormat.Clear:
                        user.SetPassword(new ClearPasswordHelper());
                        break;
                    case PasswordFormat.Encrypted:
                        throw new NotImplementedException();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                _userRepository.Save(user);
            }
            return notification;
        }

    }
}