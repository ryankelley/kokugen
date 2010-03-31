using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Kokugen.Core.Domain;
using Kokugen.Core.Services;
using Kokugen.Web.Conventions;

namespace Kokugen.Web.Actions.Board
{
    public class ListAction
    {
        private readonly IProjectService _projectService;
        private readonly IBoardService _boardService;

        public ListAction(IProjectService projectService, IBoardService boardService)
        {
            _projectService = projectService;
            _boardService = boardService;
        }

        public BoardListModel Query(BoardListModel model)
        {

            
                return new BoardListModel
                           {
                               Id = model.Id,
                               BoardColumns = _projectService.GetProjectFromId(model.Id).GetAllBoardColumns().Select(x => x as Core.Domain.BoardColumn).ToList()
                           };
            
        }

        public BoardConfigurationModel Configure(BoardConfigurationModel model)
        {
            return new BoardConfigurationModel
            {
                Id = model.Id,
                BoardColumns = _projectService.GetProjectFromId(model.Id).GetAllBoardColumns().Select(x => x as Core.Domain.BoardColumn).ToList()
            };
        }

        public AjaxResponse ReOrder(BoardColumnReorderModel model)
        {
            
            var data = new JavaScriptSerializer().Deserialize<List<ColumnOrderDTO>>(model.columns);

            _boardService.ReorderColumns(model.ProjectId, data);


            return new AjaxResponse();
        }
       
    }



    public class BoardColumnReorderModel
    {
        public string columns { get; set; }

        public Guid ProjectId { get; set; }

        //public IEnumerable<CustomBoardColumn> columns { get; set; }
    }

    public class BoardConfigurationModel : IRequestById
    {
        public Guid Id { get; set; }
        public IEnumerable<Core.Domain.BoardColumn> BoardColumns { get; set; }
    }
}