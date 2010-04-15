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
        INotification SaveCard(Card card);
        Card GetCard(Guid id);
        bool ReOrderCards(List<CardViewDTO> cards);
    }

    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IValidator _validator;
        private readonly IProjectService _projectService;

        public CardService(ICardRepository cardRepository, IValidator validator, IProjectService projectService)
        {
            _cardRepository = cardRepository;
            _validator = validator;
            _projectService = projectService;
        }

        public IEnumerable<Card> GetCards()
        {
            return _cardRepository.Query();
        }

        public IEnumerable<Card> GetCards(Project project)
        {
            return _cardRepository.Query().Where(c => c.Project == project);
        }

        public INotification SaveCard(Card card)
        {
            var validationResults = _validator.Validate(card);
            if (validationResults.IsValid())
                _cardRepository.SaveAndFlush(card);
            return validationResults;
        }

        public Card GetCard(Guid id)
        {
            return _cardRepository.Query().Where(x => x.Id == id).FirstOrDefault();
            //return _cardRepository.Get(id);
        }

        public bool ReOrderCards(List<CardViewDTO> cards)
        {
            cards.Each(x =>
                           {
                               var card = _cardRepository.Get(x.Id);
                               card.CardOrder = x.CardOrder;
                               _cardRepository.Save(card);

                           });


            return true;
        }
    }
}