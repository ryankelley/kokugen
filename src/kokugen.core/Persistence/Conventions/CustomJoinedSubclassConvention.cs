using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Kokugen.Core.Persistence.Conventions
{
    public class CustomJoinedSubclassConvention : IJoinedSubclassConvention
    {
        public void Apply(IJoinedSubclassInstance instance)
        {
            instance.Key.Column("Id");
        }
    }
}