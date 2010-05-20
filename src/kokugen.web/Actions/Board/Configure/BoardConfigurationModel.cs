using System;
using System.Collections.Generic;
using Kokugen.Web.Conventions;

namespace Kokugen.Web.Actions.Board.Configure
{
    public class BoardConfigurationModel : ProjectBaseViewModel, IRequestById
    {
        public Guid Id { get; set; }
        public IEnumerable<BoardColumnDTO> BoardColumns { get; set; }
    }
}