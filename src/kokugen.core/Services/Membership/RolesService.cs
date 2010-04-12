using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Create(IRole roleName)
        {
            ValidateAndSave(roleName);
        }

        private void ValidateAndSave(IRole role)
        {
            var entity = role as Domain.Role;

            if(entity == null)
                return;

            var validation = _validator.Validate(entity);
            if(validation.IsValid())
                _roleRepository.Save(entity);
        }

        private void ValidateAndSave(Domain.User entity)
        {
            var validation = _validator.Validate(entity);
            if (validation.IsValid())
                _userRepository.Save(entity);
        }

        public void AddToRole(IUser userName, IRole roleName)
        {
            var entity = userName as Domain.User;

            if(entity == null)
                return;

            entity.AddRole(_roleRepository.FindBy(x => x.Name, roleName.Name));

            ValidateAndSave(entity);
        }

        public void RemoveFromRole(IUser userName, IRole roleName)
        {
            var entity = userName as Domain.User;

            if (entity == null)
                return;

            //TODO: figure out how to remove a many to many

         }

        public void Delete(IRole roleName)
        {
            var entity = roleName as Domain.Role;
            _roleRepository.Delete(entity);
        }

        public IEnumerable<string> FindAll()
        {
            return from r in _roleRepository.Query()
                   select r.Name;
        }

        public IEnumerable<string> FindByUserName(IUser userName)
        {
            //TODO: figure out the query for this, likely has to be hql or some type of map/reduce linq query
            return null;
        }

        public IEnumerable<string> FindUserNamesByRole(IRole roleName)
        {
            return _roleRepository.FindBy(x => x.Name, roleName.Name)
                .GetUsers()
                .Select(x => x.UserName);
        }

        public bool IsInRole(IUser userName, IRole roleName)
        {
            return _userRepository.FindBy(x => x.UserName, userName.UserName)
                .GetRoles().Select(x => x.Name).Contains(roleName.Name);
        }

        #endregion
    }
}