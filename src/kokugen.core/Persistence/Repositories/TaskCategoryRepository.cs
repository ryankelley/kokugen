using Kokugen.Core.Domain;
using NHibernate;

namespace Kokugen.Core.Persistence.Repositories
{
    public interface ITaskCategoryRepository : IRepository<TaskCategory>
    {

    }

    public class TaskCategoryRepository : NHibernateRepository<TaskCategory>, ITaskCategoryRepository
    {
        public TaskCategoryRepository(ISession session)
            : base(session)
        {
        }
    }

    
}