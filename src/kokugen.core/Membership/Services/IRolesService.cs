using System;
using System.Collections.Generic;
using System.Web.Security;
using Kokugen.Core.Membership.Security;
using Kokugen.Core.Persistence.Repositories;
using Kokugen.Core.Validation;

namespace Kokugen.Core.Membership.Services
{
    public interface IRolesService
    {
        void Create(string roleName);
        void AddToRole(MembershipUser user, string roleName);
        void RemoveFromRole(MembershipUser user, string roleName);
        void Delete(string roleName);

        IEnumerable<string> FindAll();
        IEnumerable<string> FindByUser(MembershipUser user);
        IEnumerable<string> FindByUserName(string userName);
        IEnumerable<string> FindUserNamesByRole(string roleName);
        bool IsInRole(MembershipUser user, string roleName);
    }

    //public class RoleService : IRolesService
    //{

    //    private readonly IRoleRepository _roleRepository;
    //    private readonly IValidator _validator;

    //    public RoleService(IRoleRepository userRepository, IValidator validator)
    //    {
    //        _roleRepository = userRepository;
    //        _validator = validator;
    //    }

    //    private INotification ValidateAndSave(Role role)
    //    {
    //        var notification = _validator.Validate(role);
    //        if (notification.IsValid())
    //        {
    //            _roleRepository.Save(role);
    //        }
    //        return notification;
    //    }

    //    public INotification Create(Role role)
    //    {
    //        var notification = new Notification();

    //        if(_roleRepository.GetRoleByName(role.Name)!= null)
    //        {
    //            notification.RegisterMessage("Name", "A role with this name already exists", Severity.Error);
    //            return notification;
    //        }

    //        if (Retrieve(role.Id) == null)
    //            return ValidateAndSave(role);
            
    //        notification.RegisterMessage("Id", "This user already exists!", Severity.Error);
    //        return notification;
            
    //    }

    //    public INotification Update(Role role)
    //    {
    //        if (role.Id != Guid.Empty)
    //        {
    //            return ValidateAndSave(role);
    //        }

    //        var notification = new Notification();
    //        notification.RegisterMessage("Id", "Entity does not exist yet, cannot update.", Severity.Error);
    //        return notification;
    //    }

    //    public INotification Delete(Role role)
    //    {
    //        if (Retrieve(role.Id) != null)
    //            _roleRepository.Delete(role);

    //        var notification = new Notification();
    //        notification.RegisterMessage("Id", "Entity does not exist, cannot delete.", Severity.Error);
    //        return notification;
    //    }

    //    public Role Retrieve(Guid Id)
    //    {
    //        return _roleRepository.Get(Id);
    //    }
    //}
}