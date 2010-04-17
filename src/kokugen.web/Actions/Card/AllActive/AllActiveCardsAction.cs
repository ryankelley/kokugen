using System;
using System.Linq;
using AutoMapper;
using Kokugen.Core.Services;
using Kokugen.Web.Actions.Card.Lists;

namespace Kokugen.Web.Actions.Card.AllActive
{
    public class AllActiveCardsAction
    {
        private readonly IProjectService _projectService;

        public AllActiveCardsAction(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public AjaxResponse Command(ActiveCardsRequest model)
        {
            var project = _projectService.GetProjectFromId(model.Id);

            var cards = project.GetCards().Where(x => x.Started != null && x.DateCompleted == null).OrderBy(x => x.CardNumber).Select(x => new {Id = x.Id, Name = x.CardNumber + "-" + x.Title});
            
            return new AjaxResponse()
            {
                Success = true,
                Item = cards
            };
        }
    }

    public class ActiveCardsRequest
    {
        public Guid Id { get; set; }
    }
}