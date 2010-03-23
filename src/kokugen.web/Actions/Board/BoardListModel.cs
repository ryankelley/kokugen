using System;
using System.Collections.Generic;

namespace Kokugen.Web.Actions.Board
{
    public class BoardListModel
    {
        public BoardListModel()
        {
        }

        public BoardListModel(Guid projectId)
        {
            ProjectId = projectId;
        }

        public IEnumerable<Kokugen.Core.Domain.CustomBoardColumn> BoardColumns { get; set; }
        public Guid ProjectId { get; set; }
    }
}