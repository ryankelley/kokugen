using Kokugen.Core.Domain;
using NHibernate;

namespace Kokugen.Core.Persistence.Repositories
{
    public interface IBoardColumnRepository : IRepository<BoardColumn>
    {
        
    }

    public class BoardColumnRepository : NHibernateRepository<BoardColumn>, IBoardColumnRepository
    {
        public BoardColumnRepository(ISession session) : base(session)
        {
        }
    }
}