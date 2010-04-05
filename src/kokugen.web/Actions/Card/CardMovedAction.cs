using System;
using Kokugen.Core.Services;
using Kokugen.Web.Conventions;

namespace Kokugen.Web.Actions.Card
{
    public class CardMovedAction
    {
        private readonly ICardService _cardService;
        private readonly IBoardService _boardService;

        public CardMovedAction(ICardService cardService, IBoardService boardService)
        {
            _cardService = cardService;
            _boardService = boardService;
        }

        public AjaxResponse Move(CardMoveInputModel model)
        {
            var card = _cardService.GetCard(model.Id);
            var column = _boardService.GetColumn(model.ColumnId);

            card.Column = column;

            _cardService.SaveCard(card);

            return new AjaxResponse { Success = true};
        }

        public AjaxResponse Color(CardColorChangeInput model)
        {
            var card = _cardService.GetCard(model.Id);
            card.Color = model.Color;
            _cardService.SaveCard(card);

            return new AjaxResponse(){Success = true};
        }
    }

    public class CardColorChangeInput
    {
        public Guid Id { get; set; }
        public string Color { get; set; }
    }

    public class CardMoveInputModel
    {
        public Guid Id { get; set; }
        public Guid ColumnId { get; set; }
    }
}