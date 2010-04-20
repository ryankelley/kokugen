using System;

namespace Kokugen.Core.Domain
{
    public class Task : Entity
    {
        public virtual string Description { get; set; }
        public virtual int TaskOrder { get; set; }
        public virtual DateTime? CompletedDate { get; set; }
        public virtual string UserName { get; set; }
        public virtual bool IsComplete { get; set; }

        public virtual Card Card { get; set; }
    }
}