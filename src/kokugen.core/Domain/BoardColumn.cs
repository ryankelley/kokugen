using System;

namespace Kokugen.Core.Domain
{
    public class BoardColumn : Entity
    {
        public virtual string Name { get; set; }
        public virtual int Order { get; set; }
        public virtual int Limit { get; set; }

        public virtual Project Project { get; set; }
    }
}