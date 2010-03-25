using System;
using System.Linq;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.Board
{
    public class ListAction
    {
        private readonly IProjectService _projectService;

        public ListAction(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public BoardListModel Query(BoardSelectModel model)
        {

            if(model.Id != Guid.Empty)
            {
                return new BoardListModel
                           {
                               Id = model.Id,
                               BoardColumns = _projectService.GetProjectFromId(model.Id).GetBoardColumns().ToList()
                           };
            }
            return new BoardListModel();
        }
    }
}