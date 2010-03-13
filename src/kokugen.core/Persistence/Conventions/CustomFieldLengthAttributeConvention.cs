using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Kokugen.Core.Persistence.Conventions
{
    public class CustomFieldLengthAttributeConvention : AttributePropertyConvention<CustomFieldLengthAttribute>
    {
        protected override void Apply(CustomFieldLengthAttribute attribute, IPropertyInstance instance)
        {
            instance.Length(attribute.Length);
        }
    }
}