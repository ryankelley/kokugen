using Kokugen.Core.Validation;
using NUnit.Framework;

namespace Kokugen.Tests.Validation
{
    [TestFixture]
    public class GreaterThanZeroAttributeTester
    {
        [Test]
        public void IgnoreValidationOnNullValue()
        {
            var target = new GreaterThanZeroTarget();
            var notification = Validator.ValidateObject(target);
            Assert.IsTrue(notification.IsValid());
        }

        [Test]
        public void NegativeValidation()
        {
            var target = new GreaterThanZeroTarget(-3);
            var notification = Validator.ValidateObject(target);
            Assert.IsFalse(notification.IsValid());

            var expected = new NotificationMessage("Amount", "Must be a positive number");
            Assert.AreEqual(expected, notification.AllMessages[0]);
            Assert.AreEqual(1, notification.AllMessages.Length);
        }

        [Test]
        public void PositiveValidationWithNonNullGreaterThanZeroNumber()
        {
            var target = new GreaterThanZeroTarget(100);
            INotification notification = Validator.ValidateObject(target);
            Assert.IsTrue(notification.IsValid());
        }
    }

    public class GreaterThanZeroTarget
    {
        public GreaterThanZeroTarget()
        {
        }

        public GreaterThanZeroTarget(double? amount)
        {
            Amount = amount;
        }

        [GreaterThanZero]
        public double? Amount { get; set; }
    }
}