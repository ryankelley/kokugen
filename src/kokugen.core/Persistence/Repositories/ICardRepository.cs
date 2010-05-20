using System;
using System.Collections.Generic;
using System.Linq;
using Kokugen.Core.Domain;
using NHibernate;

namespace Kokugen.Core.Persistence.Repositories
{
    public interface ICardRepository : IRepository<Card>
    {
        IEnumerable<CumalitiveFlowData> GetCumalitiveFlowForProject(Guid projectId);
    }

    public class CardRepository : NHibernateRepository<Card>, ICardRepository
    {
        public CardRepository(ISession session) : base(session)
        {
            
        }

        public IEnumerable<CumalitiveFlowData> GetCumalitiveFlowForProject(Guid projectId)
        {
            return ExecuteStoredProcedure(flowDataConverter, "GetCumalitiveFlowForProject", new[] {new Parameter("projectId", projectId)})
                .OrderBy(x => x.ColumnId)
                .ThenBy(x => x.Day)
                .ToList();
        }

        private CumalitiveFlowData flowDataConverter(SafeDataReader input)
        {
            var output = new CumalitiveFlowData();

            if(input != null)
            {
                output.Day = input.GetDateTime(0);
                output.ColumnId = input.GetGuid(1);
                output.ColumnName = input.GetString(2);
                output.NumberOfCards = input.GetInt32(3);
            }
            return output;
        }
    }

    public class CumalitiveFlowData
    {
        public virtual DateTime Day { get; set; }
        public virtual Guid ColumnId { get; set; }
        public virtual string ColumnName { get; set; }
        public virtual int NumberOfCards { get; set; }
    }

    public interface ITaskRepository : IRepository<Task>
    {

    }

    public class TaskRepository : NHibernateRepository<Task>, ITaskRepository
    {
        public TaskRepository(ISession session)
            : base(session)
        {

        }
    }
}