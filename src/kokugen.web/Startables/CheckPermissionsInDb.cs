using System.Linq;
using FluentNHibernate.Utils;
using Kokugen.Core;
using Kokugen.Core.Domain;
using Kokugen.Core.Membership;
using Kokugen.Core.Persistence.Repositories;

namespace Kokugen.Web.Startables
{
    public class CheckPermissionsInDb : IStartable
    {
        private readonly IPermissionRepository _permissionRepository;

        public CheckPermissionsInDb(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public void Start()
        {
            var allPerms = Enumeration.GetAll<PermissionName>();

            var permsInDb = _permissionRepository.Query().ToList();

            allPerms.Each(x =>
                              {
                                  var dbPerm = permsInDb.Where(y => y.Name == x).FirstOrDefault();

                                  if (dbPerm == null)
                                  {
                                      _permissionRepository.Save(new Permission {Name = x});
                                  }
                              });
        }
    }
}