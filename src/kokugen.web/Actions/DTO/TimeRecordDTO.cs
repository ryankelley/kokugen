using System;
using Kokugen.Core.Domain;

namespace Kokugen.Web.Actions.DTO
{
    public class TimeRecordDTO
    {
        public virtual double Duration { get; set; }
        public virtual DateTime StartTime { get; set; }
        public virtual DateTime EndTime { get; set; }

        public virtual string Description { get; set; }
        public virtual Core.Domain.Project Project { get; set; }
        public virtual Core.Domain.TaskCategory Task { get; set; }

        public virtual User User { get; set; }
    }
}