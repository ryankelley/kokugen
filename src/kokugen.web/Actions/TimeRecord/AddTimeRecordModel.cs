using System;

namespace Kokugen.Web.Actions.TimeRecord
{
    public class AddTimeRecordModel
    {
        public string TimeRecordDescription { get; set; }
        //public Core.Domain.TaskCategory TimeRecordTask { get; set; }
        public Guid ProjectId { get; set; }
        public Guid TaskId { get; set; }
    }
}