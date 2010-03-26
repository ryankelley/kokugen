using System;
using System.Collections.Generic;
using System.Linq;
using Kokugen.Core.Domain;
using Kokugen.Core.Services;
using Kokugen.Web.Conventions;

namespace Kokugen.Web.Actions.Board
{
    public class ListAction
    {
        private readonly IProjectService _projectService;

        public ListAction(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public BoardListModel Query(BoardListModel model)
        {

            
                return new BoardListModel
                           {
                               Id = model.Id,
                               BoardColumns = _projectService.GetProjectFromId(model.Id).GetAllBoardColumns().Select(x => x as BoardColumn).ToList()
                           };
            
        }

        public BoardConfigurationModel Configure(BoardConfigurationModel model)
        {
            return new BoardConfigurationModel
            {
                Id = model.Id,
                BoardColumns = _projectService.GetProjectFromId(model.Id).GetAllBoardColumns().Select(x => x as BoardColumn).ToList()
            };
        }

       
    }

    public class BoardConfigurationModel : IRequestById
    {
        public Guid Id { get; set; }
        public IEnumerable<BoardColumn> BoardColumns { get; set; }
    }
}