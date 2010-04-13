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
                .ForeignKeyConstraintNames("fk_user_to_role", "fk_role_to_user")
                .Cascade.All().Table("RoleToUser");
            mapping.Map(x => x.UserName).Unique();
            mapping.Map(x => x.Email).Unique();
        }

        #endregion
    }
}