using System;
using System.Collections.Generic;

namespace Kokugen.Core.Services
{
    public class CardViewDTO
    {
        public string Title { get; set; }
        public string Details { get; set; }
        public  int TimeEstimate { get; set; }
        public int Size { get; set; }
        public string Priority { get; set; }
        public DateTime? Deadline { get; set; }
        public int CardNumber { get; set; }
        public Guid Id { get; set; }
        public Guid ColumnId { get; set; }
        public string ColumnName { get; set; }
        public string Color { get; set; }
        public string Status { get; set; }
        public string BlockReason { get; set; }
        public int CardOrder { get; set; }
        public Guid ProjectId { get; set; }

        public string GravatarHash { get; set; }
        public string UserDisplay { get; set; }

        public IEnumerable<TaskDTO> GetTasks { get; set; }
    }

    public class TaskDTO
    {
        public virtual Guid Id { get; set; }
        public virtual string Description { get; set; }
        public virtual int TaskOrder { get; set; }
        public virtual DateTime? CompletedDate { get; set; }
        public virtual string UserName { get; set; }
        public virtual bool IsComplete { get; set; }
    }
}