using System;
using Kokugen.Core;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.Board.SaveColumn
{
    public class SaveColumnAction
    {
        private readonly IProjectService _projectService;
        private readonly IBoardService _boardService;

        public SaveColumnAction(IProjectService projectService, IBoardService boardService)
        {
            _projectService = projectService;
            _boardService = boardService;
        }

        public AjaxResponse Command(BoardColumnInputModel model)
        {
            if (model.ProjectId.IsEmpty()) return new AjaxResponse { Success = false };

            var column = _boardService.ModifyColumn(model.ProjectId, model.Id, model.ColumnName, model.ColumnDescription, model.ColumnCardLimit);

            var jsonOutput = new BoardColumnDTO {Id = column.Id, Name = column.Name, Description = column.Description, CardLimit = column.CardLimit};

            return new AjaxResponse
            {
                Success = true,
                Item = jsonOutput
            };
        }
    }

    public class BoardColumnInputModel
    {
        public Guid ProjectId { get; set; }
        public Guid Id { get; set; }
        public string ColumnName { get; set; }
        public int ColumnCardLimit { get; set; }
        public string ColumnDescription { get; set; }
    }
}