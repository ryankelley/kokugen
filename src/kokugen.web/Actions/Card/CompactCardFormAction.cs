using System;
using FubuMVC.Core;
using Kokugen.Core.Services;
using AutoMapper;
namespace Kokugen.Web.Actions.Card
{
    public class CompactCardFormAction
    {
        private readonly ICardService _cardService;
        private readonly IProjectService _projectService;

        public CompactCardFormAction(ICardService cardService, IProjectService projectService)
        {
            _cardService = cardService;
            _projectService = projectService;
        }

        [FubuPartial]
        public CompactCardFormModel CompactCardForm(CompactCardFormInput model)
        {
            return new CompactCardFormModel {ProjectId = model.Id};
        }

        public AjaxResponse Save(CompactCardFormModel model)
        {
            var project = _projectService.GetProjectFromId(model.ProjectId);
            
            var card = new Core.Domain.Card();
            card.Title = model.Card.Title;
            card.Size = model.Card.Size;
            card.Priority = model.Card.Priority;
            card.Deadline = model.Card.Deadline;
            card.AssignedTo = model.Card.AssignedTo;
            card.Details = model.Card.Details;

            
            var output = new CardViewDTO();
            Mapper.DynamicMap(card, output);

            return new AjaxResponse {Success = true, Item = output};


            
        }
    }

    public class CardViewDTO
    {
        public string Title { get; set; }
        public string Details { get; set; }
        public  int TimeEstimate { get; set; }
        public int Size { get; set; }
        public string Priority { get; set; }
        public DateTime? Deadline { get; set; }
    }

    public class CompactCardFormInput
    {
        public Guid Id { get; set; }
    }

    public class CompactCardFormModel
    {
        public Guid ProjectId { get; set; }
        public Core.Domain.Card Card { get; set; }
    }
}