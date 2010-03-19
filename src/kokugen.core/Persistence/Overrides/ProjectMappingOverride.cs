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
        }
    }
}