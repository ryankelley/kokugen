using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Kokugen.Core.Domain;
using Kokugen.Core.Services;
using Kokugen.Web.Actions.Card;
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
            output.Id = project.Id;
            output.BackLog = Mapper.DynamicMap<BoardColumnDTO>(project.Backlog);
            output.Archive = Mapper.DynamicMap < BoardColumnDTO > (project.Archive);
            output.Columns = project.GetBoardColumns().Select(x => Mapper.DynamicMap<BoardColumnDTO>(x));

            output.AllCards = project.GetCards().Select(x => Mapper.Map<Core.Domain.Card, CardViewDTO>(x)).ToList();

            return output;
        }
    }

    public class ViewBoardModel
    {
        public Guid Id { get; set; }
        public BoardColumnDTO BackLog { get; set; }

        public BoardColumnDTO Archive { get; set; }

        public IEnumerable<BoardColumnDTO> Columns { get; set; }

        public IEnumerable<CardViewDTO> AllCards { get; set; }
    }

    public class ViewBoardInputModel : IRequestById
    {
        public Guid Id { get; set; }
    }
}