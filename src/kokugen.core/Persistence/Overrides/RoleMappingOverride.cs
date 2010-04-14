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
                .ForeignKeyConstraintNames("fk_role_to_user", "fk_user_to_role")
                .Cascade.All().Table("RoleToUser");
            mapping.Map(x => x.Name).Unique();

            mapping.HasManyToMany(x => x.Permissions).Access.CamelCaseField(Prefix.Underscore).Cascade.SaveUpdate().Table("RoleToPermission").ForeignKeyConstraintNames("fk_role_to_permission", "fk_permission_to_role").BatchSize(5);
        }

        #endregion
    }

    
}