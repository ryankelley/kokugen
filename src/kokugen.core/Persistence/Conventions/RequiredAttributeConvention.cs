using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using Kokugen.Core.Validation;

namespace Kokugen.Core.Persistence.Conventions
{
    public class RequiredAttributeConvention: AttributePropertyConvention<RequiredAttribute>
    {
        protected override void Apply(RequiredAttribute attribute, IPropertyInstance instance)
        {
            instance.Not.Nullable();
        }
    }
}