using System;

namespace Kokugen.Web.Actions.Board.ReOrder
{
    public class BoardColumnReorderModel
    {
        public string columns { get; set; }

        public Guid ProjectId { get; set; }

        //public IEnumerable<CustomBoardColumn> columns { get; set; }
    }
}