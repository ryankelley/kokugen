using System;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Mapping;
using Kokugen.Core.Domain;

namespace Kokugen.Core.Persistence.Overrides
{
    public class CardMappingOverride : IAutoMappingOverride<Card>
    {
        public void Override(AutoMapping<Card> mapping)
        {
            mapping.Map(x => x.Details).CustomType("StringClob").Length(10000);
            mapping.Map(x => x.CardNumber).Generated.Always().Not.Update().Not.Insert().Not.LazyLoad().ReadOnly().CustomSqlType("int identity").Not.Nullable();
            mapping.References(x => x.Column, "Column_id").Cascade.SaveUpdate().ForeignKey("fk_card_to_column");
            mapping.References(x => x.Project).Cascade.SaveUpdate().ForeignKey("fk_card_to_project");
            mapping.References(x => x.AssignedTo).Cascade.SaveUpdate().Fetch.Join().ForeignKey("fk_Card_to_AssignedTo");
            mapping.HasMany(x => x.GetTasks()).Access.CamelCaseField(Prefix.Underscore).Inverse().Cascade.SaveUpdate().ForeignKeyConstraintName("fk_card_to_task").BatchSize(20);
            mapping.HasMany(x => x.GetActivities()).Access.CamelCaseField(Prefix.Underscore).Inverse().Cascade.SaveUpdate().ForeignKeyConstraintName("fk_card_to_activity").BatchSize(20);
        }
    }

    public class TaskMappingOverride : IAutoMappingOverride<Task>
    {
        public void Override(AutoMapping<Task> mapping)
        {
            mapping.References(x => x.Card).Cascade.SaveUpdate().ForeignKey("fk_task_to_card");
        }
    }

    public class CardActivityMappingOverride : IAutoMappingOverride<CardActivity>
    {
        public void Override(AutoMapping<CardActivity> mapping)
        {
            mapping.References(x => x.Card).Cascade.SaveUpdate().ForeignKey("fk_activity_to_card");
            mapping.References(x => x.Leaving).Cascade.SaveUpdate().ForeignKey("fk_activity_to_columnin");
            mapping.References(x => x.Entering).Cascade.SaveUpdate().ForeignKey("fk_activity_to_columnout");
        }
    }
}