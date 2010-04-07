#region

using System;
using System.Linq;
using System.Web.Security;
using Kokugen.Core.Membership.Security;
using Kokugen.Core.Persistence.Repositories;
using Kokugen.Core.Validation;

#endregion

namespace Kokugen.Core.Membership.Services
{
    public interface IUserService
    {
        INotification Create(MembershipUser user);
        INotification Update(MembershipUser user);
        INotification Delete(MembershipUser user);
        MembershipUser Retrieve(Guid Id);

        MembershipUser GetUserByLogin(string name);
        MembershipUser GetUserByEmail(string email);
    }

    //public class UserService : IUserService
    //{
    //    private readonly IUserRepository _userRepository;
    //    private readonly IValidator _validator;

    //    public UserService(IUserRepository userRepository, IValidator validator)
    //    {
    //        _userRepository = userRepository;
    //        _validator = validator;
    //    }

    //    private INotification ValidateAndSave(User user)
    //    {
    //        var notification = _validator.Validate(user);
    //        if (notification.IsValid())
    //        {
    //            _userRepository.Save(user);
    //        }
    //        return notification;
    //    }

    //    public INotification Create(User user)
    //    {
    //        var notification = new Notification();
    //        if (GetUserByLogin(user.Login) != null)
    //        {
    //            notification.RegisterMessage("UserName", "This username is taken", Severity.Error);
    //            return notification;
    //        }
    //        if (Retrieve(user.Id) == null)
    //            return ValidateAndSave(user);
            
            
    //        notification.RegisterMessage("Id", "This user already exists!", Severity.Error);
    //        return notification;
            
    //    }

    //    public INotification Update(User user)
    //    {
    //        if (user.Id != Guid.Empty)
    //        {
    //            return ValidateAndSave(user);
    //        }

    //        var notification = new Notification();
    //        notification.RegisterMessage("Id", "Entity does not exist yet, cannot update.", Severity.Error);
    //        return notification;
    //    }

    //    public INotification Delete(User user)
    //    {
    //        if (Retrieve(user.Id) != null)
    //            _userRepository.Delete(user);

    //        var notification = new Notification();
    //        notification.RegisterMessage("Id", "Entity does not exist, cannot delete.", Severity.Error);
    //        return notification;
    //    }

    //    public User Retrieve(Guid Id)
    //    {
    //        return _userRepository.Get(Id);
    //    }

    //    public User GetUserByLogin(string name)
    //    {
    //        return _userRepository.Query().Where(u => u.Login == name).FirstOrDefault();
    //    }

    //    public User GetUserByEmail(string email)
    //    {
    //        return _userRepository.Query().Where(u => u.EmailAddress == email).FirstOrDefault();
    //    }
    //}
}