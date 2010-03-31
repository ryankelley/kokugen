using System.Linq;
using Kokugen.Core.Domain;
using NHibernate;

namespace Kokugen.Core.Persistence.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Role GetRoleByName(string name);
    }

    public class RoleRepository : NHibernateRepository<Role>, IRoleRepository
    {
        public RoleRepository(ISession session)
            : base(session)
        {

        }

        public Role GetRoleByName(string name)
        {
            return Query().Where(u => u.Name == name).FirstOrDefault();
        }
    }
}