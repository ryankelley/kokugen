using System;

namespace Kokugen.Core.Domain
{
    public class DailyTimeRecord : Entity
    {
        public virtual double Duration { get; set; }
        public virtual DateTime StartTime { get; set; }
        public virtual DateTime? EndTime { get; set; }
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
            Duration = Math.Round((time.Days * 24) + time.Hours + ((double)time.Minutes / 60) + ((double)time.Seconds / 3600), 2); // gets timespent in hours
        }
        
    }
}