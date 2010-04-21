using System;

namespace Kokugen.Web.Actions.TimeRecord
{
    public class AddTimeRecordModel
    {
        public DateTime StartTime;
        public string TimeRecordDescription { get; set; }
        //public Core.Domain.TaskCategory TimeRecordTask { get; set; }
        public Guid ProjectId { get; set; }
        public Guid TaskId { get; set; }
        public Guid UserId { get; set; }
        public Guid CardId { get; set; }
        public Guid Id { get; set; }
    }
}