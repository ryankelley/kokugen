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

            return Mapper.Map<Core.Domain.Card, CardDetailModel>(card);
            
        }
    }

    public class CardDetailModel : ProjectBaseViewModel
    {
        public string Title { get; set; }
        public string Details { get; set; }
        public int TimeEstimate { get; set; }
        public int Size { get; set; }
        public string Priority { get; set; }
        public DateTime? Deadline { get; set; }
        public int CardNumber { get; set; }
        public Guid Id { get; set; }
        public Guid ColumnId { get; set; }
        public string Color { get; set; }
        public string Status { get; set; }
        public string BlockReason { get; set; }
        public int CardOrder { get; set; }
        
    }

    public class CardDetailInputModel : IRequestById
    {
        public Guid Id
        { get; set; }
    }
}