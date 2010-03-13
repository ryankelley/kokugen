using Kokugen.Core.Domain;
using NHibernate;

namespace Kokugen.Core.Persistence.Repositories
{
    public interface ICompanyRepository : IRepository<Company>
    {

    }

    public class CompanyRepository : NHibernateRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(ISession session) : base(session)
        {
        }
    }
}