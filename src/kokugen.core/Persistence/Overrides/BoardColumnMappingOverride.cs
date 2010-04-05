using System;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Mapping;
using Kokugen.Core.Domain;

namespace Kokugen.Core.Persistence.Overrides
{
    public class BoardColumnMappingOverride : IAutoMappingOverride<BoardColumn>
    {
        public void Override(AutoMapping<BoardColumn> mapping)
        {
            mapping.HasMany(x => x.GetCards()).Access.CamelCaseField(Prefix.Underscore).Cascade.SaveUpdate().ForeignKeyConstraintName("fk_column_to_card");
        }
    }
}