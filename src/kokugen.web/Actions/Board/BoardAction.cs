using System;
using Kokugen.Core.Services;
using Kokugen.Web.Conventions;

namespace Kokugen.Web.Actions.Board
{
    public class ListAction
    {
        private readonly IProjectService _projectService;
        private readonly IValueObjectInitializer _initLists;

        public ListAction(IProjectService projectService, IValueObjectInitializer initLists)
        {
            _projectService = projectService;
            _initLists = initLists;
        }

        public BoardListModel Query(BoardListModel boardListModel)
        {
            _initLists.Start();

            if(boardListModel.ProjectId != Guid.Empty)
            {
                return new BoardListModel(boardListModel.ProjectId)
                       {
                           BoardColumns = _projectService.GetProjectFromId(boardListModel.ProjectId).GetBoardColumns()
                       };
            }
            return new BoardListModel();
        }
    }
}