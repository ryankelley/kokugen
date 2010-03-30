using System;
using System.Collections.Generic;
using Kokugen.Core.Domain;
using Kokugen.Web.Conventions;

namespace Kokugen.Web.Actions.Board
{
    public class BoardListModel : IRequestById
    {
        public Guid Id { get; set; }
        public IEnumerable<BoardColumn> BoardColumns { get; set; }
        
    }
}