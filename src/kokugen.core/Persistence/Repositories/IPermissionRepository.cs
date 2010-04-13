using Kokugen.Core.Domain;
using NHibernate;

namespace Kokugen.Core.Persistence.Repositories
{
    public interface IPermissionRepository : IRepository<Permission>
    {

    }

    public class PermissionRepository : NHibernateRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(ISession session) : base(session)
        {
        }
    }
}