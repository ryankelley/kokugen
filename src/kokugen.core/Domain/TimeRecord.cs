using System;
using System.Web.Security;
using Kokugen.Core.Membership.Security;

namespace Kokugen.Core.Domain
{
    public class TimeRecord : Entity
    {
        public virtual double Duration { get; set; }
        public virtual double Billable { get; set; }
        public virtual DateTime StartTime { get; set; }
        public virtual DateTime? EndTime { get; set; }

        public virtual string Description { get; set; }
        public virtual Project Project { get; set; }
        public virtual Card Card { get; set; }
        public virtual TaskCategory Task { get; set; }

        public virtual Guid UserId { get; set; }

        public virtual void Start()
        {
            StartTime = DateTime.Now;
        }

        public virtual void Stop()
        {
            EndTime = DateTime.Now;
        }

        public virtual void ComputeDuration()
        {
            var time = EndTime.Value.Subtract(StartTime);
            Duration = (time.Days * 24) + time.Hours + ((double)time.Minutes / 60) + ((double)time.Seconds / 3600); // gets timespent in hours
        }
    }
}