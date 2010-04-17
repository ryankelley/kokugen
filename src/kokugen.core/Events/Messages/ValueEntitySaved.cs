using Kokugen.Core.Domain;

namespace Kokugen.Core.Events.Messages
{
    public class ValueEntitySaved<T> where T : Entity
    {
        public ValueEntitySaved(T entity)
        {
            Entity = entity;
        }

        public T Entity { get; set; }
    }

    public class ValueEntityRemoved<T> where T : Entity
    {
        public ValueEntityRemoved(T entity)
        {
            Entity = entity;
        }

        public T Entity { get; set; }
    }

    public class CompanyRemoved : ValueEntityRemoved<Company>
    {
        public CompanyRemoved(Company entity) : base(entity)
        {
        }
    }
}