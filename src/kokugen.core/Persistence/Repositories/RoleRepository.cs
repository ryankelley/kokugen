using Kokugen.Core.Domain;
using NHibernate;

namespace Kokugen.Core.Persistence.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
    }

    public class RoleRepository : NHibernateRepository<Role>, IRoleRepository
    {
        public RoleRepository(ISession session) : base(session)
        {
        }
    }
}