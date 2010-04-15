using Kokugen.Core.Domain;
using NHibernate;

namespace Kokugen.Core.Persistence.Repositories
{
    public interface IDailyTimeRecordRepository : IRepository<DailyTimeRecord>
    {
        
    }

    public class DailyTimeRecordRepository : NHibernateRepository<DailyTimeRecord>,IDailyTimeRecordRepository
    {
        public DailyTimeRecordRepository(ISession session) : base(session)
        {
        }
    }
}