using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.Card.Color
{
    public class ColorAction
    {
        private readonly ICardService _cardService;

        public ColorAction(ICardService cardService)
        {
            _cardService = cardService;
        }

        public AjaxResponse Command(CardColorChangeInput model)
        {
            var card = _cardService.GetCard(model.Id);
            card.Color = model.Color;
            _cardService.SaveCard(card);

            return new AjaxResponse() { Success = true };
        }
    }
}