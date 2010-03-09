using Kokugen.Core.Validation;
using NUnit.Framework;

namespace Kokugen.Tests.Validation
{
    [TestFixture]
    public class GreaterOrEqualToZeroAttributeTester
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
        }

        #endregion

        [Test]
        public void HappyPathValidation()
        {
            var target = new GreaterOrEqualToZeroTarget();
            target.Count = 3;
            target.Rate = 5;

            Validator.AssertValid(target);
        }

        [Test]
        public void SadPathValidation()
        {
            var target = new GreaterOrEqualToZeroTarget();
            target.Count = -1;
            target.Rate = -1;

            var expected = new[]
                               {
                                   new NotificationMessage("Rate", Notification.MUST_BE_GREATER_OR_EQUAL_TO_ZERO),
                                   new NotificationMessage("Count", Notification.MUST_BE_GREATER_OR_EQUAL_TO_ZERO)
                               };

            var notification = Validator.ValidateObject(target);
            Assert.AreEqual(expected, notification.AllMessages);
        }
    }

    public class GreaterOrEqualToZeroTarget
    {
        [GreaterOrEqualToZero]
        public double? Rate { get; set; }

        [GreaterOrEqualToZero]
        public int? Count { get; set; }
    }
}