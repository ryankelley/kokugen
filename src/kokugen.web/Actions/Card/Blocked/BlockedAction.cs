using Kokugen.Core.Domain;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.Card.Blocked
{
    public class BlockedAction
    {
        private readonly ICardService _cardService;

        public BlockedAction(ICardService cardService)
        {
            _cardService = cardService;
        }

        public AjaxResponse Command(CardBlockedInput model)
        {
            var card = _cardService.GetCard(model.Id);
            card.Status = model.Status == "Blocked" ? CardStatus.Blocked : CardStatus.New;
            card.BlockReason = model.Reason;


            if (card.Column.Name != Core.Domain.BoardColumn.BacklogName && card.Column.Name != Core.Domain.BoardColumn.ArchiveName)
            {
                if (card.Status == CardStatus.Blocked)
                    card.StartIdle();
                else
                    card.StartWorking();

            }

            _cardService.SaveCard(card);
            return new AjaxResponse() { Success = true };
        }
    }
}