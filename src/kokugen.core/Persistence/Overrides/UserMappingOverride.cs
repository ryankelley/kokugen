using System;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Mapping;
using Kokugen.Core.Domain;

namespace Kokugen.Core.Persistence.Overrides
{
    public class UserMappingOverride : IAutoMappingOverride<User>
    {
        #region Implementation of IAutoMappingOverride<User>

        public void Override(AutoMapping<User> mapping)
        {
            mapping.HasManyToMany(x => x.GetRoles()).Access.CamelCaseField(Prefix.Underscore)
                .Cascade.All();
            mapping.Map(x => x.UserName).Unique();
            mapping.Map(x => x.Email).Unique();
        }

        #endregion
    }
}