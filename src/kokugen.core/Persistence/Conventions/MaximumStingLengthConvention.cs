using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using Kokugen.Core.Validation;

namespace Kokugen.Core.Persistence.Conventions
{
    public class MaximumStingLengthConvention : AttributePropertyConvention<MaximumStringLengthAttribute>
    {
        protected override void Apply(MaximumStringLengthAttribute attribute, IPropertyInstance instance)
        {
            instance.Length(attribute.Length);
        }
    }
}