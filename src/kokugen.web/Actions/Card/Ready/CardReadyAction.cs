using Kokugen.Core.Domain;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.Card.Ready
{
    public class CardReadyAction
    {
        private readonly ICardService _cardService;

        public CardReadyAction(ICardService cardService)
        {
            _cardService = cardService;
        }

        public AjaxResponse Command(CardReadyInput model)
        {
            var card = _cardService.GetCard(model.Id);
            card.Status = model.Status ? CardStatus.Ready : CardStatus.New;
            _cardService.SaveCard(card);

            return new AjaxResponse() { Success = true };
        }
    }
}