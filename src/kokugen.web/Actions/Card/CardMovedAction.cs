using System;
using System.Web.Script.Serialization;
using Kokugen.Core.Domain;
using Kokugen.Core.Services;
using Kokugen.Web.Conventions;
using System.Collections.Generic;

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

            card.Status = CardStatus.New;
            card.BlockReason = "";

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

        public AjaxResponse Ready(CardReadyInput model)
        {
            var card = _cardService.GetCard(model.Id);
            card.Status = model.Status ? CardStatus.Ready : CardStatus.New;
            _cardService.SaveCard(card);

            return new AjaxResponse() {Success = true};
        }

        public AjaxResponse Blocked(CardBlockedInput model)
        {
            var card = _cardService.GetCard(model.Id);
            card.Status = model.Status == "Blocked" ? CardStatus.Blocked : CardStatus.New;
            card.BlockReason = model.Reason;
            _cardService.SaveCard(card);
            return new AjaxResponse() { Success = true };
        }

        public AjaxResponse ReOrder(CardOrderInputModel model)
        {
            var data = new JavaScriptSerializer().Deserialize<List<CardViewDTO>>(model.Cards);

            _cardService.ReOrderCards(data);


            return new AjaxResponse();
        }
    }

    public class CardOrderInputModel
    {
        public string Cards { get; set; }
    }

    public class CardBlockedInput
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
    }

    public class CardReadyInput
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
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