using System;
using System.Linq;
using Kokugen.Core;
using Kokugen.Core.Domain;
using Kokugen.Core.Membership.Services;
using Kokugen.Core.Persistence.Repositories;

namespace Kokugen.Web.Startables
{
    public class UserInitializer : IValueObjectInitializer 
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserInitializer(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public void Start()
        {
            var users = _userRepository.Query().AsEnumerable();

            var values = from u in users
                         select new ValueObject(u.Id.ToString(), u.UserName);

            ValueObjectRegistry.AddValueObjects<User>(values);

            var roles = _roleRepository.Query().AsEnumerable();

            var roleValues = from r in roles
                             select new ValueObject(r.Id.ToString(), r.Name);

            ValueObjectRegistry.AddValueObjects<Role>(roleValues);
        }
    }
}