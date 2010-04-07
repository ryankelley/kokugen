using System;
using System.Web.Security;
using Kokugen.Core.Membership.Security;

namespace Kokugen.Core.Domain
{
    public class TimeRecord : Entity
    {
        public virtual double Duration { get; set; }
        public virtual DateTime StartTime { get; set; }
        public virtual DateTime EndTime { get; set; }

        public virtual string Description { get; set; }
        public virtual Project Project { get; set; }
        public virtual TaskCategory Task { get; set; }

        public virtual MembershipUser User { get; set; }

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
             Duration = ((double)(EndTime - StartTime).Seconds)/3600; // gets timespent in hours
        }
    }
}