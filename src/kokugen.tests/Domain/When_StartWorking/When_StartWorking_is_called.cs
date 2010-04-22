using System;
using System.Linq;
using Kokugen.Core.Domain;
using NUnit.Framework;

namespace Kokugen.Tests.Domain.When_StartWorking
{
    public class When_there_is_no_activity : CardTester
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

    public class When_the_last_acitivity_is_not_working_that_is_not_stopped : CardTester
    {
        protected override void BecauseOnce()
        {
            _card = new Card();

            _card.AddActivity(new CardActivity { StartTime = DateTime.Now, Status = ActivityType.Idle });

            _card.StartWorking();
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
            _card.GetActivities().First().Status.ShouldEqual(ActivityType.Idle);
        }

        [Test]
        public void The_last_activity_should_not_be_stopped()
        {
            _card.GetActivities().Last().EndTime.ShouldBeNull();
        }

        [Test]
        public void The_last_activity_should_be_working()
        {
            _card.GetActivities().Last().Status.ShouldEqual(ActivityType.Working);
        }
    }

    public class When_the_last_activity_is_working : CardTester
    {
        protected override void BecauseOnce()
        {
            _card = new Card();

            _card.AddActivity(new CardActivity { StartTime = DateTime.Now, Status = ActivityType.Working });

            _card.StartWorking();
        }

        [Test]
        public void There_should_be_two_activites()
        {
            _card.GetActivities().ShouldHaveCount(1);
        }

        [Test]
        public void The_first_activity_should_not_be_stopped()
        {
            _card.GetActivities().First().EndTime.ShouldBeNull();
        }

        [Test]
        public void The_activity_should_still_be_working()
        {
            _card.GetActivities().First().Status.ShouldEqual(ActivityType.Working);
        }

    }
    public class When_the_last_activity_is_stopped : CardTester
    {
        protected override void BecauseOnce()
        {
            _card = new Card();

            _card.AddActivity(new CardActivity { StartTime = DateTime.Now, EndTime = DateTime.Now, Status = ActivityType.Idle });

            _card.StartWorking();
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
        public void The_last_activity_should_not_be_stopped()
        {
            _card.GetActivities().Last().EndTime.ShouldBeNull();
        }

        [Test]
        public void The_last_activity_should_be_idle()
        {
            _card.GetActivities().Last().Status.ShouldEqual(ActivityType.Working);
        }
    }
}