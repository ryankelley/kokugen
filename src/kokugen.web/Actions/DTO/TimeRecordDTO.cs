using System;
using System.Web.Security;
using Kokugen.Core.Membership.Security;

namespace Kokugen.Web.Actions.DTO
{
    public class TimeRecordDTO
    {
        public virtual Guid Id { get; set; }
        public virtual double Duration { get; set; }
        public virtual double Billable { get; set; }
        public virtual DateTime StartTime { get; set; }
        public virtual DateTime? EndTime { get; set; }
        public virtual string Description { get; set; }
        public virtual Guid ProjectId { get; set; }
        public virtual string ProjectName { get; set; }
        public virtual Core.Domain.TaskCategory Task { get; set; }

        //public virtual MembershipUser User { get; set; }
    }
}