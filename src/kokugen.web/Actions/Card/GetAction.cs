using System;
using AutoMapper;
using Kokugen.Core;
using Kokugen.Core.Services;
using Kokugen.Web.Conventions;

namespace Kokugen.Web.Actions.Card
{
    public class GetAction
    {
        private readonly ICardService _cardService;

        public GetAction(ICardService cardService)
        {
            _cardService = cardService;
        }

        public CardDetailModel Query(CardDetailInputModel model)
        {
            if(model.Id.IsEmpty()) return new CardDetailModel();

            var card = _cardService.GetCard(model.Id);

            return new CardDetailModel {Card = Mapper.Map<Core.Domain.Card, CardViewDTO>(card), ProjectId = card.Project.Id};
        }
    }

    public class CardDetailModel : ProjectBaseViewModel
    {
        public CardViewDTO Card { get; set; }
    }

    public class CardDetailInputModel : IRequestById
    {
        public Guid Id
        { get; set; }
    }
}