using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
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
        private readonly IValidator _validator;
        private readonly MembershipSettingsBag _settings;

        public UserService(IUserRepository userRepository, 
            IPasswordHelperService passwordHelperService,
            IValidator validator,
            MembershipSettingsBag settings)
        {
            _userRepository = userRepository;
            _passwordHelperService = passwordHelperService;
            _validator = validator;
            _settings = settings;
        }

        public void Update(IUser user)
        {
            var entity = user as Domain.User;
            if (entity != null) ValidateAndSave(entity);
        }

        public void Delete(IUser user)
        {
            var entity = user as Domain.User;
            if (entity != null) _userRepository.Delete(entity);
        }

        public IUser Retrieve(object id)
        {
            return _userRepository.Get((Guid) id);
        }

        public IUser GetUserByLogin(string name)
        {
            return _userRepository.FindBy(x => x.UserName, name);
        }

        public IUser GetUserByEmail(string email)
        {
            if(!_settings.Registration.RequiresUniqueEmail)
                throw new InvalidOperationException("RegistrationSettings.RequireUniqueEmail must be true to retrieve users by email");

            return _userRepository.FindBy(x => x.Email, email);
        }

        public IPagedList<IUser> FindAll(int pageIndex, int pageSize)
        {
            var users = _userRepository.Query()
                .Take(pageSize)
                .Skip((pageIndex - 1)*pageSize);
            return new StaticPagedList<IUser>( users.Where(x => true)
                .Select(u => u as IUser), pageIndex, pageSize, TotalUsers);
        }

        public int TotalUsers
        {
            get { return _userRepository.Query().Count(); }
        }


        public INotification Create(IUser user)
        {
            var notification = new Notification();

            var entity = user as Domain.User;
            if(entity == null)
            {
                notification.RegisterMessage("User",
                                             string.Format("User cannot be cast to {0}", typeof (Domain.User).FullName),
                                             Severity.Error);
                return notification;
            }

            switch (_settings.Password.PasswordFormat)
            {
                case PasswordFormat.Hashed:
                    entity.SetPassword(_passwordHelperService);
                    break;
                case PasswordFormat.Clear:
                    entity.SetPassword(new ClearPasswordHelper());
                    break;
                case PasswordFormat.Encrypted:
                    throw new NotImplementedException();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return ValidateAndSave(entity);
        }

        private INotification ValidateAndSave(Domain.User user)
        {
            var notification = _validator.Validate(user);
            if (notification.IsValid())
            {
                //make sure user is unique
                if(_userRepository.FindBy(x => x.UserName, user.UserName) != null)
                {
                    notification.RegisterMessage("UserName", "User name already exists!", Severity.Error);
                    return notification;
                }
                _userRepository.Save(user);
            }
            return notification;
        }

    }

    public class ClearPasswordHelper : IPasswordHelperService
    {
        #region Implementation of IPasswordHelperService

        public string CreatePasswordHash(string password)
        {
            return password;
        }

        public bool ComparePasswordToHash(string password, string passwordHash)
        {
            return password == passwordHash;
        }

        public string RandomPasswordNoHash(int length)
        {
            throw new NotImplementedException();
        }

        public string RandomPasswordHashed(int length)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}