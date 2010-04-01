using System;

namespace Kokugen.Core.Domain
{
    public class TaskCategory : Entity
    {
        public virtual string Name { get; set; }
        public virtual Guid Id { get; set; }

    }
}