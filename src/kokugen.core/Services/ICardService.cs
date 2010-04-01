using System;
using System.Collections.Generic;
using System.Linq;
using Kokugen.Core.Domain;
using Kokugen.Core.Persistence.Repositories;
using Kokugen.Core.Validation;

namespace Kokugen.Core.Services
{
    public interface ICardService
    {
        IEnumerable<Card> GetCards();
        IEnumerable<Card> GetCards(Project project);
        INotification CreateCard(Card card);
    }

    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IValidator _validator;

        public CardService(ICardRepository cardRepository, IValidator validator)
        {
            _cardRepository = cardRepository;
            _validator = validator;
        }

        public IEnumerable<Card> GetCards()
        {
            return _cardRepository.Query();
        }

        public IEnumerable<Card> GetCards(Project project)
        {
            return _cardRepository.Query().Where(c => c.Project == project);
        }

        public INotification CreateCard(Card card)
        {
            var validationResults = _validator.Validate(card);
            if (validationResults.IsValid())
                _cardRepository.Save(card);
            return validationResults;
        }
    }
}