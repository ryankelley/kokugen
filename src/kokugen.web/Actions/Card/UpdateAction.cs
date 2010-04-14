using System;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.Card
{
    public class UpdateAction
    {
        private readonly ICardService _cardService;

        public UpdateAction(ICardService cardService)
        {
            _cardService = cardService;
        }

        public InPlaceAjaxResponse Update(CardInPlaceEditModel model)
        {
            var card = _cardService.GetCard(model.id);
            Type cardType = card.GetType();
            cardType.GetProperty(model.PropertyName).SetValue(card, model.PropertyValue,null);

            _cardService.SaveCard(card);

            return new InPlaceAjaxResponse { success = true, NewValueToDisplay = model.PropertyValue};
        }
    }

    public class CardInPlaceEditModel
    {
        public Guid id { get; set; }
        public string PropertyName { get; set; }
        public object PropertyValue { get; set; }
    }
}