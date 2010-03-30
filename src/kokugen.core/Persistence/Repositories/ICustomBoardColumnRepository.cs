using Kokugen.Core.Domain;
using NHibernate;

namespace Kokugen.Core.Persistence.Repositories
{
    public interface ICustomBoardColumnRepository : IRepository<CustomBoardColumn>
    {
        
    }

    public class CustomBoardColumnRepository : NHibernateRepository<CustomBoardColumn>, ICustomBoardColumnRepository
    {
        public CustomBoardColumnRepository(ISession session) : base(session)
        {
        }
    }
}