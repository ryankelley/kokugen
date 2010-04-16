using System.Collections.Generic;
using System.Linq;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.Card
{
    public class ListAction
    {
        private readonly ICardService _cardService;

        public ListAction(ICardService cardService)
        {
            _cardService = cardService;
        }

        public CardListModel Query(CardListModel inModel)
        {
            return new CardListModel()
                       {
                           Cards = _cardService.GetCards().OrderBy(x => x.CardNumber)
                       };
        }
    }

    public class CardListModel
    {
        public IEnumerable<Core.Domain.Card> Cards { get; set; }
    }
}