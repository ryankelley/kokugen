using Kokugen.Core.Domain;
using NHibernate;

namespace Kokugen.Core.Persistence.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {

    }

    public class ProjectRepository : NHibernateRepository<Project>, IProjectRepository
    {
        public ProjectRepository(ISession session) : base(session)
        {
        }
    }
}