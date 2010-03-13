using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Kokugen.Core.Persistence.Conventions
{
    public class PrimaryKeyConvention : IIdConvention
    {
        public void Apply(IIdentityInstance instance)
        {
            instance.Column("Id");
        }
    }
}