using Kokugen.Core.Domain;
using NHibernate;

namespace Kokugen.Core.Persistence.Repositories
{
    public interface ICardRepository : IRepository<Card>
    {
        
    }

    public class CardRepository : NHibernateRepository<Card>, ICardRepository
    {
        public CardRepository(ISession session) : base(session)
        {
            
        }
    }
}