using System;
using FubuMVC.Core;
using Kokugen.Core;
using Kokugen.Core.Domain;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.Board
{
    public class EditBoardColumnFormAction
    {
        private readonly IBoardService _boardService;

        public EditBoardColumnFormAction(IBoardService boardService)
        {
            _boardService = boardService;
        }

        [FubuPartial]
        public BoardColumnEditModel Execute(BoardColumnEditModel model)
        {
            if(model.Id.IsNotEmpty())
            {
                var column = _boardService.GetCustomColumn(model.Id);
                return new BoardColumnEditModel {Column = column, ProjectId = column.Project.Id};
            }
            return new BoardColumnEditModel { ProjectId = model.ProjectId};
        }
    }


    public class BoardColumnEditModel
    {
        public Guid Id { get; set; }
        public CustomBoardColumn Column { get; set; }

        public Guid ProjectId { get; set; }
    }
}