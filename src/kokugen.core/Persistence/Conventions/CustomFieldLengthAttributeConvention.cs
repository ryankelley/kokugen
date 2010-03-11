using System;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using Kokugen.Core.Validation;

namespace Kokugen.Core.Persistence.Conventions
{
    public class CustomFieldLengthAttributeConvention : AttributePropertyConvention<CustomFieldLengthAttribute>
    {
        protected override void Apply(CustomFieldLengthAttribute attribute, IPropertyInstance instance)
        {
            instance.Length(attribute.Length);
        }
    }

    public class RequiredFieldAttributeConvention : AttributePropertyConvention<RequiredAttribute>
    {
        protected override void Apply(RequiredAttribute attribute, IPropertyInstance instance)
        {
            instance.Not.Nullable();
        }
    }
}