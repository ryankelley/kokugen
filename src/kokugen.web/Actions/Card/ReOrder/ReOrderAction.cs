using System.Collections.Generic;
using System.Web.Script.Serialization;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.Card.ReOrder
{
    public class ReOrderAction
    {
        private readonly ICardService _cardService;

        public ReOrderAction(ICardService cardService)
        {
            _cardService = cardService;
        }

        public AjaxResponse Command(CardOrderInputModel model)
        {
            var data = new JavaScriptSerializer().Deserialize<List<CardViewDTO>>(model.Cards);

            _cardService.ReOrderCards(data);


            return new AjaxResponse();
        }
    }
}