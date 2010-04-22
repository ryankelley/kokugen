using System;
using Kokugen.Core.Domain;
using Kokugen.Core.Services;
using Kokugen.Web.Actions.Card;
using Kokugen.Web.Actions.Card.Blocked;
using Kokugen.Web.Actions.Card.Ready;
using NUnit.Framework;
using StructureMap.AutoMocking;
using Rhino.Mocks;

namespace Kokugen.Tests.Actions.Card
{
    [TestFixture]
    public class CardBlockedAction_Tester : ContextSpecification
    {
        protected RhinoAutoMocker<BlockedAction> _mocks;
        protected BlockedAction _action;
        protected ICardService _cardService;
        protected Core.Domain.Card _card;

        protected override void SetContext()
        {
            _mocks = new RhinoAutoMocker<BlockedAction>();
            _action = _mocks.ClassUnderTest;

            _cardService = _mocks.Get<ICardService>();
            _card = _mocks.Get<Core.Domain.Card>();
        }

       
        public class When_the_card_is_blocked_in_work_column : CardBlockedAction_Tester
        {
            private CardBlockedInput _input;
            private Guid cardId;

            protected override void Because()
            {
                cardId = Guid.NewGuid();
                _input = new CardBlockedInput { Id = cardId, Status = "Blocked" };



                _card.Stub(c => c.Status).PropertyBehavior();
                _card.Stub(c => c.Column).Return(ActionStubs.WorkColumn);

                _cardService.Stub(x => x.GetCard(cardId)).Return(_card);
                _action.Command(_input);
            }

            [Test]
            public void Should_get_card_from_card_service()
            {
                _cardService.AssertWasCalled(c => c.GetCard(cardId));
            }

            [Test]
            public void Should_set_card_status_to_Blocked()
            {
                _card.Status.ShouldEqual(CardStatus.Blocked);
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

        public class When_the_card_is_not_blocked_in_a_working_column : CardBlockedAction_Tester
        {
            private CardBlockedInput _input;
            private Guid cardId;

            protected override void Because()
            {
                cardId = Guid.NewGuid();
                _input = new CardBlockedInput { Id = cardId, Status = "new" };
                _card.Stub(c => c.Status).PropertyBehavior();
                _card.Stub(c => c.Column).Return(ActionStubs.WorkColumn);
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

    public static class ActionStubs
    {
        public static CustomBoardColumn WorkColumn = new CustomBoardColumn{ ColumnOrder = 1, CardLimit = 3, Name = "Working"};
        public static BoardColumn BacklogColumn = new CustomBoardColumn{ ColumnOrder = 0, CardLimit = 0, Name = BoardColumn.BacklogName};
        public static BoardColumn ArchiveColumn = new CustomBoardColumn{ ColumnOrder = 0, CardLimit = 0, Name = BoardColumn.ArchiveName};

    }
}