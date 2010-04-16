using System;
using FubuMVC.Core;
using FubuMVC.Core.Security;
using Kokugen.Core;
using Kokugen.Core.Attributes;
using Kokugen.Core.Domain;
using Kokugen.Core.Membership.Services;
using Kokugen.Core.Services;
using AutoMapper;
namespace Kokugen.Web.Actions.Card
{
    public class CompactCardFormAction
    {
        private readonly ICardService _cardService;
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;
        private readonly ISecurityContext _securityContext;

        public CompactCardFormAction(ICardService cardService, IProjectService projectService, IUserService userService, ISecurityContext securityContext)
        {
            _cardService = cardService;
            _projectService = projectService;
            _userService = userService;
            _securityContext = securityContext;
        }

        [FubuPartial]
        public CompactCardFormModel CompactCardForm(CompactCardFormInput model)
        {
            return new CompactCardFormModel {ProjectId = model.Id};
        }

        public AjaxResponse Save(CompactCardFormModel model)
        {
            var project = _projectService.GetProjectFromId(model.ProjectId);

            var user = model.UserId.IsEmpty() ? _userService.GetUserByLogin(_securityContext.CurrentIdentity.Name) : _userService.GetUserById(model.UserId);

            var card = new Core.Domain.Card
                           {
                               Title = model.Card.Title, 
                               Size = model.Card.Size, 
                               Priority = model.Card.Priority, 
                               Deadline = model.Card.Deadline, 
                               Details = model.Card.Details,
                               Project = project,
                               Color = "grey",
                               Status = CardStatus.New,
                               AssignedTo = user 
                               
                           };

            
            var notification = _cardService.SaveCard(card);
            if (notification.IsValid())
            {
                card.Column = project.Backlog;
                project.AddCard(card);
                _projectService.SaveProject(project);
                var output = new CardViewDTO();
                Mapper.DynamicMap(card, output);

                return new AjaxResponse {Success = true, Item = output};
            }
            return new AjaxResponse {Success = false};

        }
    }

    public class CompactCardFormInput
    {
        public Guid Id { get; set; }
    }

    public class CompactCardFormModel
    {
        public Guid ProjectId { get; set; }
        public Core.Domain.Card Card { get; set; }

        [ValueOf("User")]
        public Guid UserId { get; set; }

        public string Submit { get; set; }
    }
}