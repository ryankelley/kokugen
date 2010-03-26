using System;
using System.Collections.Generic;
using Kokugen.Core.Domain;
using Kokugen.Web.Conventions;

namespace Kokugen.Web.Actions.Board
{
    public class BoardListModel
    {
        public Guid Id { get; set; }
        public IEnumerable<CustomBoardColumn> BoardColumns { get; set; }
        
    }

    public class BoardSelectModel : IRequestById
    {
        public Guid Id { get; set; }
    }
}