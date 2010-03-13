using System;
using System.Reflection;
using Kokugen.Core.Validation;
using NUnit.Framework;

namespace Kokugen.Tests.Validation
{
    public class NotificationAssertion : Notification
    {
        public void AssertEquals(INotification notification)
        {
            Assert.AreEqual(AllMessages, notification.AllMessages);
        }
    }

    [TestFixture]
    public class RequiredAttributeTester
    {
        private Func<RequiredAttribute, PropertyInfo, string> _messageBuilder;

        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
            _messageBuilder = RequiredAttribute.GetMessage;
        }

        [TearDown]
        public void TearDown()
        {
            RequiredAttribute.GetMessage = _messageBuilder;
        }

        #endregion

        [Test]
        public void Customize_the_message_builder()
        {
            RequiredAttribute.GetMessage = (att, prop) => prop.Name + " is required";

            var expected = new NotificationAssertion();
            expected.RegisterMessage("Name", "Name is required", Severity.Error);
            expected.RegisterMessage("Amount", "Amount is required", Severity.Error);
            expected.RegisterMessage("TradeDate", "TradeDate is required", Severity.Error);

            var target = new RequiredPropertyTarget();


            var actual = Validator.ValidateObject(target);
            expected.AssertEquals(actual);
        }

        [Test]
        public void AllNegative()
        {
            var expected = new NotificationAssertion();
            expected.RegisterMessage("Name", Notification.REQUIRED_FIELD, Severity.Error);
            expected.RegisterMessage("Amount", Notification.REQUIRED_FIELD, Severity.Error);
            expected.RegisterMessage("TradeDate", Notification.REQUIRED_FIELD, Severity.Error);

            var target = new RequiredPropertyTarget();


            var actual = Validator.ValidateObject(target);
            expected.AssertEquals(actual);
        }


        [Test]
        public void Empty_string_fails_validation()
        {
            var expected = new NotificationAssertion();
            expected.RegisterMessage("Name", Notification.REQUIRED_FIELD, Severity.Error);

            var target = new RequiredPropertyTarget(){Amount = 3, TradeDate = DateTime.Today, Name = string.Empty};


            var actual = Validator.ValidateObject(target);
            expected.AssertEquals(actual);
        }

        [Test]
        public void AllPositive()
        {
            var expected = new NotificationAssertion();

            var target = new RequiredPropertyTarget();
            target.Amount = 3;
            target.Name = "Jeremy";

            target.TradeDate = DateTime.Now;

            var actual = Validator.ValidateObject(target);
            expected.AssertEquals(actual);
        }


        [Test]
        public void AllPositive2()
        {
            var expected = new NotificationAssertion();

            var target = new RequiredPropertyTarget();
            target.Amount =0;
            target.Name = "Jeremy";

            target.TradeDate = DateTime.Now;

            var actual = Validator.ValidateObject(target);
            expected.AssertEquals(actual);
        }

        [Test]
        public void IsRequired()
        {
            RequiredAttribute.IsRequired(typeof (RequiredPropertyTarget).GetProperty("Name")).ShouldBeTrue();
            RequiredAttribute.IsRequired(typeof (RequiredPropertyTarget).GetProperty("Other")).ShouldBeFalse();
        }

        [Test]
        public void NotificationMessages_Are_A_Warning_When_The_Severity_Is_Specified_In_The_Attribute()
        {
            var target2 = new RequiredPropertyTarget2();
            target2.TradeDate = null;

            Validator.ValidateField(target2, "TradeDate")[0].Severity.ShouldEqual(Severity.Warning);
        }

        [Test]
        public void NotificationMessages_Are_An_Error_If_No_Severity_Is_Specified_In_The_Attribute()
        {
            var target2 = new RequiredPropertyTarget2();
            target2.Amount = null;

            Validator.ValidateField(target2, "Amount")[0].Severity.ShouldEqual(Severity.Error);
        }


        [Test]
        public void NotificationMessages_Are_An_Error_When_The_Severity_Is_Specified_In_The_Attribute()
        {
            var target2 = new RequiredPropertyTarget2();
            target2.Name = null;

            Validator.ValidateField(target2, "Name")[0].Severity.ShouldEqual(Severity.Error);
        }
    }

    public class DifferentRequiredAttribute : RequiredAttribute
    {
        protected override void validate(object target, object rawValue, INotification notification)
        {
            // no op
        }
    }

    internal class RequiredPropertyTarget
    {
        [DifferentRequired]
        public string Other { get; set; }

        [Required]
        public string Name { get; set; }


        [Required]
        public double? Amount { get; set; }

        [Required]
        public DateTime? TradeDate { get; set; }
    }

    internal class RequiredPropertyTarget2
    {
        [Required(Severity = Severity.Error)]
        public string Name { get; set; }


        [Required()]
        public double? Amount { get; set; }

        [Required(Severity = Severity.Warning)]
        public DateTime? TradeDate { get; set; }
    }
}