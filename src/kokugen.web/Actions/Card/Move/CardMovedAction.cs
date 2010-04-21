using Kokugen.Core.Domain;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.Card.Move
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

        public AjaxResponse Command(CardMoveInputModel model)
        {
            var card = _cardService.GetCard(model.Id);
            var column = _boardService.GetColumn(model.ColumnId);

            if (column.Name == "Archive")
                card.Status = CardStatus.Complete;
            else
                card.Status = CardStatus.New;
            
            card.BlockReason = "";

            card.Column = column;

            _cardService.SaveCard(card);

            return new AjaxResponse { Success = true };
        }
    }
}