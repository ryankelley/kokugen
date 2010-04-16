using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Kokugen.Core.Services;
using Kokugen.Web.Conventions;

namespace Kokugen.Web.Actions.Card
{
    public class ListAction
    {
        private readonly ICardService _cardService;
        private readonly IProjectService _projectService;

        public ListAction(ICardService cardService, IProjectService projectService)
        {
            _cardService = cardService;
            _projectService = projectService;
        }

        public CardListModel Query(CardListModel inModel)
        {

            var project = _projectService.GetProjectFromId(inModel.Id);

            var cards = _cardService.GetCards(project).OrderBy(x => x.CardNumber) ;

            var cardData = new List<CardViewDTO>();
                cards.Each(x => cardData.Add(Mapper.DynamicMap<Core.Services.CardViewDTO>(x)));

            return new CardListModel()
                       {
                           Cards = cardData,
                           ProjectId = inModel.Id
                       };
        }
    }

    public class CardListModel : ProjectBaseViewModel, IRequestById 
    {
        public IEnumerable<Core.Services.CardViewDTO> Cards { get; set; }

        public Guid Id { get; set; }
    }
}