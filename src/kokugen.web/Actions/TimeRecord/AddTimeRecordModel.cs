using System;

namespace Kokugen.Web.Actions.TimeRecord
{
    public class AddTimeRecordModel
    {
        public string TimeRecordDescription { get; set; }
        public Core.Domain.TaskCategory TimeRecordTask { get; set; }
        //public Core.Domain.User TimeRecordUser { get; set; }
        public Guid TimeRecordProjectId { get; set; }

    }
}