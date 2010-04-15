using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FubuMVC.Core;
using Kokugen.Core.Services;
using Kokugen.Web.Actions.Board;

namespace Kokugen.Web.Actions.Card
{
    public class CardProgressAction
    {
        private readonly IProjectService _projectService;
        private readonly IBoardService _boardService;

        public CardProgressAction(IProjectService projectService, IBoardService boardService)
        {
            _projectService = projectService;
            _boardService = boardService;
        }

        [FubuPartial]
        public CardProgressModel Execute(CardProgressModel model)
        {
            var project = _projectService.GetProjectFromId(model.ProjectId);

            var backLog = Mapper.DynamicMap<BoardColumnDTO>(project.Backlog);
            var archive = Mapper.DynamicMap<BoardColumnDTO>(project.Archive);
            var columns = project.GetBoardColumns().OrderBy(a => a.ColumnOrder).Select(x => Mapper.DynamicMap<BoardColumnDTO>(x));

            var outputList = new List<BoardColumnDTO>();
            outputList.Add(backLog);
            outputList.AddRange(columns);
            outputList.Add(archive);

            return new CardProgressModel { CurrentColumnId = model.CurrentColumnId, ProjectId = model.ProjectId, Columns = outputList};

        }
    }

    public class CardProgressModel
    {
        public Guid CurrentColumnId { get; set; }
        public Guid ProjectId { get; set; }

        public IEnumerable<BoardColumnDTO> Columns { get; set; }
    }
}