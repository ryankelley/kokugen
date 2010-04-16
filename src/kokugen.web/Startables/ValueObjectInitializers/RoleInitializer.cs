using System;
using System.Linq;
using Kokugen.Core;
using Kokugen.Core.Domain;
using Kokugen.Core.Membership.Services;
using Kokugen.Core.Persistence.Repositories;

namespace Kokugen.Web.Startables
{
    public class RoleInitializer : IValueObjectInitializer 
    {

        private readonly IRoleRepository _roleRepository;

        public RoleInitializer(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public void Start()
        {

            var roles = _roleRepository.Query().AsEnumerable();

            var roleValues = from r in roles
                             select new ValueObject(r.Id.ToString(), r.Name);

            ValueObjectRegistry.AddValueObjects<Role>(roleValues);
        }
    }
}