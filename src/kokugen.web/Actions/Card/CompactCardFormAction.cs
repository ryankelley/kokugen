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
            
            var card = new Core.Domain.Card
                           {
                               Title = model.Card.Title, 
                               Size = model.Card.Size, 
                               Priority = model.Card.Priority, 
                               Deadline = model.Card.Deadline, 
                               //AssignedTo = model.Card.AssignedTo, 
                               Details = model.Card.Details,
                               Project = project
                               
                           };

            
            var notification = _cardService.SaveCard(card);
            var newcard = _cardService.GetCard(card.Id);
            if (notification.IsValid())
            {
                project.Backlog.AddCard(card);
                _projectService.SaveProject(project);
                var output = new CardViewDTO();
                Mapper.DynamicMap(card, output);

                return new AjaxResponse {Success = true, Item = output};
            }
            return new AjaxResponse {Success = false};

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
        public int CardNumber { get; set; }
    }

    public class CompactCardFormInput
    {
        public Guid Id { get; set; }
    }

    public class CompactCardFormModel
    {
        public Guid ProjectId { get; set; }
        public Core.Domain.Card Card { get; set; }
        public string Submit { get; set; }
    }
}