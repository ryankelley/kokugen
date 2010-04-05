using System;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Kokugen.Core.Domain;

namespace Kokugen.Core.Persistence.Overrides
{
    public class TimeRecordMappingOverride : IAutoMappingOverride<TimeRecord>
    {
        public void Override(AutoMapping<TimeRecord> mapping)
        {
            mapping.References(a => a.Task, "Task_id").Cascade.SaveUpdate().Fetch.Join().ForeignKey("fk_TimeRecord_to_Task");
        }
    }
}