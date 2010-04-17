using System;

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
    }
}