using System;

namespace Kokugen.Core.Domain
{
    public class BoardColumn : Entity
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public int Limit { get; set; }

        public Project Project { get; set; }
    }
}