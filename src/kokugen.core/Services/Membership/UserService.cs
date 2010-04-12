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
            if (entity != null) _userRepository.Save(entity);
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
            return _userRepository.FindBy(x => x.Email, email);
        }

        public IPagedList<IUser> FindAll(int pageIndex, int pageSize)
        {
            var users = _userRepository.Query()
                .Take(pageSize)
                .Skip((pageIndex - 1)*pageSize);

            //var converter = new EnumerableToEnumerableTConverter<IEnumerable<User>, IUser>();
            //var usersList = converter.ConvertTo<IEnumerable<IUser>>(users);

            return new StaticPagedList<IUser>( users.Where(x => true).Select(u => u as IUser), pageIndex, pageSize, _userRepository.Query().Count());
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

            entity.HashPassword(_passwordHelperService);

            return ValidateAndSave(entity);
        }

        private INotification ValidateAndSave(Domain.User user)
        {
            var notification = _validator.Validate(user);
            if (notification.IsValid())
            {
                _userRepository.Save(user);
            }
            return notification;
        }

    }
}