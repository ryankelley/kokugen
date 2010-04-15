using System;
using System.Collections.Generic;
using System.Linq;
using Kokugen.Core.Domain;
using Kokugen.Core.Membership.Security;
using Kokugen.Core.Membership.Services;
using Kokugen.Core.Persistence.Repositories;
using Kokugen.Core.Validation;
using NHibernate.Criterion;

namespace Kokugen.Core.Services
{
    public class RolesService : IRolesService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IValidator _validator;

        public RolesService(IRoleRepository roleRepository, IUserRepository userRepository, IValidator validator)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _validator = validator;
        }

        #region Implementation of IRolesService

        public void Create(Role roleName)
        {
            ValidateAndSave(roleName);
        }

        private void ValidateAndSave(Role entity)
        {

            if(entity == null)
                return;

            var validation = _validator.Validate(entity);
            if (validation.IsValid())
            {
                //ensure role.Name is unique
                if (_roleRepository.FindBy(x => x.Name, entity.Name) == null)
                    _roleRepository.Save(entity);

            }
        }

        private void ValidateAndSave(Domain.User entity)
        {
            var validation = _validator.Validate(entity);
            if (validation.IsValid())
                _userRepository.Save(entity);
        }

        public void AddUserToRole(User userName, Role roleName)
        {
            var entity = userName as Domain.User;

            if(entity == null)
                return;

            entity.AddRole(_roleRepository.FindBy(x => x.Name, roleName.Name));

            ValidateAndSave(entity);
        }

        public void RemoveUserFromRole(User userName, Role roleName)
        {
            var entity = userName as Domain.User;

            if (entity == null)
                return;

            //TODO: figure out how to remove a many to many

         }

        public void Delete(Role roleName)
        {
            var entity = roleName as Domain.Role;
            _roleRepository.Delete(entity);
        }

        public IEnumerable<string> FindAll()
        {
            return _roleRepository.FindAll().Select(x => x.Name);
        }

        public IEnumerable<string> FindByUserName(User userName)
        {
            return _userRepository.FindBy(x => x.UserName, userName.UserName)
                .GetRoles().Select(x => x.Name);
        }

        public IEnumerable<string> FindUserNamesByRole(Role roleName)
        {
            return _roleRepository.FindBy(x => x.Name, roleName.Name)
                .GetUsers()
                .Select(x => x.UserName);
        }

        public bool IsInRole(User userName, Role roleName)
        {
            return _userRepository.FindBy(x => x.UserName, userName.UserName)
                .GetRoles().Select(x => x.Name).Contains(roleName.Name);
        }

        #endregion
    }
}