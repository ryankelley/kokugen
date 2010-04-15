using System;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Mapping;
using Kokugen.Core.Domain;

namespace Kokugen.Core.Persistence.Overrides
{
    public class ProjectMappingOverride : IAutoMappingOverride<Project>
    {
        public void Override(AutoMapping<Project> mapping)
        {
            mapping.HasMany(x => x.GetTimeRecords())
                .Access.CamelCaseField(Prefix.Underscore)
                .Cascade.SaveUpdate()
                .ForeignKeyConstraintName("FK_Project_To_Time_Record");

            mapping.HasMany(x => x.GetBoardColumns())
                .Access.CamelCaseField(Prefix.Underscore)
                .Cascade.SaveUpdate()
                .ForeignKeyConstraintName("FK_Project_To_Board_Columns");

            mapping.HasMany(x => x.GetCards()).Access.CamelCaseField(Prefix.Underscore).Cascade.SaveUpdate().ForeignKeyConstraintName("fk_column_to_card").Inverse();

            mapping.HasManyToMany(x => x.GetUsers())
                .Access.CamelCaseField(Prefix.Underscore)
                .Cascade.SaveUpdate()
                .ForeignKeyConstraintNames("FK_Project_To_User_Columns", "FK_User_To_Project_Columns");
        }
    }
}