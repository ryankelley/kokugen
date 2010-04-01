using Kokugen.Core.Domain;
using NHibernate;

namespace Kokugen.Core.Persistence.Repositories
{
    namespace Kokugen.Core.Persistence.Repositories
    {
        public interface ITimeRecordRepository : IRepository<TimeRecord>
        {

        }

        public class TimeRecordRepository : NHibernateRepository<TimeRecord>, ITimeRecordRepository
        {
            public TimeRecordRepository(ISession session)
                : base(session)
            {
            }
        }


    }
}