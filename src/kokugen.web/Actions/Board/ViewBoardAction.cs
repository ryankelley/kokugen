using System;
using System.Collections.Generic;
using Kokugen.Core.Domain;
using Kokugen.Core.Services;
using Kokugen.Web.Conventions;

namespace Kokugen.Web.Actions.Board
{
    public class ViewBoardAction
    {
        private readonly IProjectService _projectService;

        public ViewBoardAction(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public ViewBoardModel Query(ViewBoardInputModel model)
        {
            var project = _projectService.GetProjectFromId(model.Id);

            var output = new ViewBoardModel();

            output.BackLog = project.Backlog;
            output.Archive = project.Archive;
            output.Columns = project.GetBoardColumns();

            return output;
        }
    }

    public class ViewBoardModel
    {
        public Core.Domain.BoardColumn BackLog { get; set; }

        public Core.Domain.BoardColumn Archive { get; set; }

        public IEnumerable<CustomBoardColumn> Columns { get; set; }
    }

    public class ViewBoardInputModel : IRequestById
    {
        public Guid Id { get; set; }
    }
}