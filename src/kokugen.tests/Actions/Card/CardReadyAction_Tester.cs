using System;
using Kokugen.Core.Domain;
using Kokugen.Core.Services;
using Kokugen.Web.Actions.Card;
using Kokugen.Web.Actions.Card.Ready;
using NUnit.Framework;
using StructureMap.AutoMocking;
using Rhino.Mocks;

namespace Kokugen.Tests.Actions.Card
{
    [TestFixture]
    public class CardReadyAction_Tester : ContextSpecification
    {
        protected RhinoAutoMocker<CardReadyAction> _mocks;
        protected CardReadyAction _action;
        protected ICardService _cardService;
        protected Core.Domain.Card _card;

        protected override void SetContext()
        {
            _mocks = new RhinoAutoMocker<CardReadyAction>();
            _action = _mocks.ClassUnderTest;

            _cardService = _mocks.Get<ICardService>();
            _card = _mocks.Get<Core.Domain.Card>();
        }

       
        public class When_the_card_is_ready : CardReadyAction_Tester
        {
            private CardReadyInput _input;
            private Guid cardId;

            protected override void Because()
            {
                cardId = Guid.NewGuid();
                _input = new CardReadyInput {Id = cardId, Status = true};



                _card.Stub(c => c.Status).PropertyBehavior();

                _cardService.Stub(x => x.GetCard(cardId)).Return(_card);
                _action.Command(_input);
            }

            [Test]
            public void Should_get_card_from_card_service()
            {
                _cardService.AssertWasCalled(c => c.GetCard(cardId));
            }

            [Test]
            public void Should_set_card_status_to_Ready()
            {
                _card.Status.ShouldEqual(CardStatus.Ready);
            }

            [Test]
            public void Should_start_idle_time_on_card()
            {
                _card.AssertWasCalled(c => c.StartIdle());
            }

            [Test]
            public void Should_save_the_card()
            {
                _cardService.AssertWasCalled(c => c.SaveCard(_card));
            }

        }

        public class When_the_card_is_not_ready : CardReadyAction_Tester
        {
            private CardReadyInput _input;
            private Guid cardId;

            protected override void Because()
            {
                cardId = Guid.NewGuid();
                _input = new CardReadyInput { Id = cardId, Status = false };
                _card.Stub(c => c.Status).PropertyBehavior();

                _cardService.Stub(x => x.GetCard(cardId)).Return(_card);
                _action.Command(_input);
            }

            [Test]
            public void Should_get_card_from_card_service()
            {
                _cardService.AssertWasCalled(c => c.GetCard(cardId));
            }

            [Test]
            public void Should_set_card_status_to_New()
            {
                _card.Status.ShouldEqual(CardStatus.New);
            }

            [Test]
            public void Should_start_working_time_on_card()
            {
                _card.AssertWasCalled(c => c.StartWorking());
            }

            [Test]
            public void Should_save_the_card()
            {
                _cardService.AssertWasCalled(c => c.SaveCard(_card));
            }
        }
    }
}