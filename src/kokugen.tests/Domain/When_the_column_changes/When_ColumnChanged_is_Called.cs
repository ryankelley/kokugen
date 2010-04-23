using System;
using System.Linq;
using Kokugen.Core.Domain;
using NUnit.Framework;

namespace Kokugen.Tests.Domain.When_the_column_changes
{
    public class When_ColumnChanged_is_Called : CardTester
    {
        protected override void BecauseOnce()
        {
            _card = new Card();
            _card.StartWorking();
        }

        [Test]
        public void There_should_only_be_one_activity()
        {
            _card.GetActivities().ShouldHaveCount(1);
        }

        [Test]
        public void The_activity_should_be_working()
        {
            _card.GetActivities().First().Status.ShouldEqual(ActivityType.Working);
        }
    }

    public class When_the_last_acitivity_is_a_column_change : CardTester
    {
        protected override void BecauseOnce()
        {
            _card = new Card();

            _card.AddActivity(new CardActivity { StartTime = DateTime.Now, Status = ActivityType.Column, Leaving = Stubs.BacklogColumn});

            _card.Column = Stubs.WorkColumn;
            _card.ColumnChanged(Stubs.BacklogColumn, Stubs.WorkColumn);
        }

        [Test]
        public void There_should_be_two_activites()
        {
            _card.GetActivities().ShouldHaveCount(2);
        }

        [Test]
        public void The_first_activity_should_be_stopped()
        {
            _card.GetActivities().First().EndTime.ShouldNotBeNull();
        }

        [Test]
        public void the_first_activity_should_be_idle()
        {
            _card.GetActivities().First().Status.ShouldEqual(ActivityType.Column);
        }

        [Test]
        public void the_first_activity_should_be_from_the_backlog_column()
        {
            _card.GetActivities().First().Leaving.ShouldEqual(Stubs.BacklogColumn);
        }

        [Test]
        public void The_last_activity_should_not_be_stopped()
        {
            _card.GetActivities().Last().EndTime.ShouldBeNull();
        }

        [Test]
        public void The_last_activity_should_be_working()
        {
            _card.GetActivities().Last().Status.ShouldEqual(ActivityType.Column);
        }

        [Test]
        public void the_last_activity_should_be_from_the_working_column()
        {
            _card.GetActivities().Last().Entering.ShouldEqual(Stubs.WorkColumn);
        }
    }

    public class When_there_is_no_last_activity : CardTester
    {
        protected override void BecauseOnce()
        {
            _card = new Card();

            _card.Column = Stubs.BacklogColumn;

            _card.ColumnChanged(null, Stubs.BacklogColumn);
        }

        [Test]
        public void There_should_be_one_activity()
        {
            _card.GetActivities().ShouldHaveCount(1);
        }

        [Test]
        public void The_first_activity_should_not_be_stopped()
        {
            _card.GetActivities().First().EndTime.ShouldBeNull();
        }

        [Test]
        public void The_activity_should_still_be_column()
        {
            _card.GetActivities().First().Status.ShouldEqual(ActivityType.Column);
        }

        [Test]
        public void The_activity_should_be_for_the_backlog_column()
        {
            _card.GetActivities().First().Entering.ShouldEqual(Stubs.BacklogColumn);
        }

    }
   
}