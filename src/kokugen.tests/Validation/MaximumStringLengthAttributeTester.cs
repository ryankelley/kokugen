using System.Reflection;
using Kokugen.Core.Validation;
using NUnit.Framework;

namespace Kokugen.Tests.Validation
{
    [TestFixture]
    public class MaximumStringLengthAttributeTester
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
        }

        #endregion

        private void assertMaximumLengthOfProperty(string propertyName, int length)
        {
            PropertyInfo property = typeof (LongStringTarget).GetProperty(propertyName);
            MaximumStringLengthAttribute.GetLength(property).ShouldEqual(length);
        }

        [Test]
        public void GetTheLengthFromProperty()
        {
            assertMaximumLengthOfProperty("Name", 7);
            assertMaximumLengthOfProperty("LastName", 100);
        }

        [Test]
        public void Longer_name_than_the_specification_throws_validation_error()
        {
            var target = new LongStringTarget {Name = "Bartholomew"};
            NotificationMessage[] messages = Validator.ValidateField(target, "Name");
            messages.Length.ShouldEqual(1);

            NotificationMessage message = messages[0];
            message.FieldName.ShouldEqual("Name");
            message.Message.ShouldEqual("Name cannot be longer than 7 characters");
        }

        [Test]
        public void Null_does_not_throw_any_validation_errors()
        {
            var target = new LongStringTarget {Name = null};
            Validator.ValidateObject(target).AllMessages.Length.ShouldEqual(0);
        }

        [Test]
        public void Short_name_exactly_the_specified_length_does_not_throw_any_validation_errors()
        {
            var target = new LongStringTarget {Name = "Roberta"};
            Validator.ValidateObject(target).AllMessages.Length.ShouldEqual(0);
        }

        [Test]
        public void Short_name_less_than_specified_length_does_not_throw_any_validation_errors()
        {
            var target = new LongStringTarget {Name = "Jeremy"};
            Validator.ValidateObject(target).AllMessages.Length.ShouldEqual(0);
        }
    }

    public class LongStringTarget
    {
        [MaximumStringLength(7)]
        public string Name { get; set; }


        public string LastName { get; set; }

        public int Age { get; set; }
    }
}