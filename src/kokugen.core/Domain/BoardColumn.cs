using System;
using System.Collections.Generic;
using System.Linq;
using Kokugen.Core.Validation;

namespace Kokugen.Core.Domain
{
    

    [Serializable]
    public class BoardColumn : Entity
    {
        public const string BacklogName = "Backlog";
        public const string ArchiveName = "Archive";

        [Required]
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual int CardLimit { get; set; }

        
    }

    [Serializable]
    public class CustomBoardColumn : BoardColumn
    {
        
        public virtual int ColumnOrder { get; set; }
        public virtual Project Project { get; set; }
    }

}