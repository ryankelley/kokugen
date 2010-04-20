using System;

namespace Kokugen.Web.Actions.TimeRecord
{
    public class StopTimeRecordModel
    {
        public Guid Id { get; set; }

        public double Duration { get; set; }

        public double Billable { get; set; }

        public string Description { get; set; }
        

    }
}