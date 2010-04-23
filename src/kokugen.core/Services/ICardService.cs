using System;
using System.Collections.Generic;
using System.Linq;
using Kokugen.Core.Domain;
using Kokugen.Core.Persistence.Repositories;
using Kokugen.Core.Validation;
using NHibernate;

namespace Kokugen.Core.Services
{
    public interface ICardService
    {
        IEnumerable<Card> GetCards();
        IEnumerable<Card> GetCards(Project project);
        INotification SaveCard(Card card);
        Card GetCard(Guid id);
        bool ReOrderCards(List<CardViewDTO> cards);
        Card CreateCard(Card card, Project project, User user);
    }

    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IValidator _validator;
        private readonly IProjectService _projectService;
        private readonly ISession _session;

        public CardService(ICardRepository cardRepository, IValidator validator, IProjectService projectService, ISession session)
        {
            _cardRepository = cardRepository;
            _validator = validator;
            _projectService = projectService;
            _session = session;
        }

        public IEnumerable<Card> GetCards()
        {
            return _cardRepository.Query();
        }

        public IEnumerable<Card> GetCards(Project project)
        {
            return _session.CreateCriteria<Card>()
               .SetFetchMode("GetTasks", FetchMode.Eager)
               .Add(NHibernate.Criterion.Expression.Eq("Project", project))
                .List<Card>();
            //return _cardRepository.Query().Where(c => c.Project == project);
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

        public Card CreateCard(Card card, Project project, User user)
        {
            var newcard = new Card
            {
                Title = card.Title,
                Size = card.Size,
                Priority = card.Priority,
                Deadline = card.Deadline,
                Details = card.Details,
                Project = project,
                Color = "grey",
                Status = CardStatus.New,
                AssignedTo = user

            };

            var lastCard = project.GetCards().OrderByDescending(x => x.CardNumber).Take(1).FirstOrDefault();

            newcard.CardNumber = lastCard == null ? 1 : lastCard.CardNumber + 1;

            return newcard;
        }
    }
}