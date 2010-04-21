using System;
using Kokugen.Core;
using Kokugen.Core.Domain;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.Card.Dates
{
    public class CardDatesAction
    {
        private readonly ICardService _cardService;

        public CardDatesAction(ICardService cardService)
        {
            _cardService = cardService;
        }

        /// <summary>
        /// This function updates the dates on a card, possible values for status are "Started, NotStarted, Done, and NotDone"
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public AjaxResponse Command(CardDateInputModel model)
        {
            if (model.Id.IsEmpty()) return new AjaxResponse() { Success = false };

            var card = _cardService.GetCard(model.Id);

            switch (model.Status)
            {
                case "Started":
                    card.Started = DateTime.Now;
                    break;

                case "NotStarted":
                    card.DateCompleted = null;
                    card.Started = null;
                    break;

                case "Done":
                    card.DateCompleted = DateTime.Now;
                    card.Status = CardStatus.Complete;
                    break;

                case "NotDone":
                    card.DateCompleted = null;
                    break;

            }

            _cardService.SaveCard(card);

            return new AjaxResponse() { Success = true };
        }
    }


}