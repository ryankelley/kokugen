using System;
using Kokugen.Core.Validation;

namespace Kokugen.Core.Domain
{
    

    public class BoardColumn : Entity
    {
        [Required]
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }

    }

    public class CustomBoardColumn : BoardColumn
    {
        public virtual int ColumnOrder { get; set; }
        public virtual Project Project { get; set; }
    }

}