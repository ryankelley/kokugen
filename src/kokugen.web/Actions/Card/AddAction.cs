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

        public AjaxResponse Command(CardInputFormModel inModel)
        {
            return new AjaxResponse()
                       {
                           Item = inModel.Card,
                           Success = true
                       };
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