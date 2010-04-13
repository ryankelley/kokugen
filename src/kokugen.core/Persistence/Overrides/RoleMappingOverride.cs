using System;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Mapping;
using Kokugen.Core.Domain;

namespace Kokugen.Core.Persistence.Overrides
{
    public class RoleMappingOverride : IAutoMappingOverride<Role>
    {
        #region Implementation of IAutoMappingOverride<Role>

        public void Override(AutoMapping<Role> mapping)
        {
            mapping.HasManyToMany(x => x.GetUsers()).Access.CamelCaseField(Prefix.Underscore)
                .Cascade.All();
            mapping.Map(x => x.Name).Unique();
        }

        #endregion
    }
}