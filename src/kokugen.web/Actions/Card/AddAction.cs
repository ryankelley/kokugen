using FubuMVC.Core.Continuations;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.Card
{
    public class AddAction
    {
        private readonly ICardService _cardService;

        public AddAction(ICardService cardService)
        {
            _cardService = cardService;
        }

        public CardInputFormModel Command(CardInputFormModel inModel)
        {

            return new CardInputFormModel();
        }
    }

    public class CardInputFormModel
    {
        public Core.Domain.Card Card { get; set; }
    }

    public class CardFormModel
    {
        public Core.Domain.Card Card { get; set; }
    }
}