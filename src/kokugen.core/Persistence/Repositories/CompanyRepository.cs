using Kokugen.Core.Domain;
using Kokugen.Core.Events;
using NHibernate;

namespace Kokugen.Core.Persistence.Repositories
{
    public interface ICompanyRepository : IRepository<Company>
    {

    }

    public class CompanyRepository : PublishingRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(ISession session, IEventAggregator eventAggregator) : base(session, eventAggregator)
        {
        }
    }
}