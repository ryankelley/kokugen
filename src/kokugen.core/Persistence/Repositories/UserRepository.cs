using Kokugen.Core.Domain;
using NHibernate;

namespace Kokugen.Core.Persistence.Repositories
{
    public interface IUserRepository : IRepository<User>{}

    public class UserRepository : NHibernateRepository<User>, IUserRepository
    {
        public UserRepository(ISession session) : base(session)
        {
        }
    }
   
}