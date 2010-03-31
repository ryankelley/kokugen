using System.Reflection;
using Kokugen.Core.Validation;
using NUnit.Framework;

namespace Kokugen.Tests.Validation
{
    [TestFixture]
    public class MinimumStringLengthAttributeTester
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
        }

        #endregion

        private void assertMinimumLengthOfProperty(string propertyName, int length)
        {
            PropertyInfo property = typeof(ShortStringTarget).GetProperty(propertyName);
            MinimumStringLengthAttribute.GetLength(property).ShouldEqual(length);
        }

        [Test]
        public void GetTheLengthFromProperty()
        {
            assertMinimumLengthOfProperty("Name", 6);
        }

        [Test]
        public void Shorter_name_than_the_specification_throws_validation_error()
        {
            var target = new ShortStringTarget { Name = "Bart" };
            NotificationMessage[] messages = Validator.ValidateField(target, "Name");
            messages.Length.ShouldEqual(1);

            NotificationMessage message = messages[0];
            message.FieldName.ShouldEqual("Name");
            message.Message.ShouldEqual("Name cannot be shorter than 6 characters");
        }

        [Test]
        public void Null_does_not_throw_any_validation_errors()
        {
            var target = new ShortStringTarget { Name = null };
            Validator.ValidateObject(target).AllMessages.Length.ShouldEqual(0);
        }

        [Test]
        public void Short_name_exactly_the_specified_length_does_not_throw_any_validation_errors()
        {
            var target = new ShortStringTarget { Name = "Roberta" };
            Validator.ValidateObject(target).AllMessages.Length.ShouldEqual(0);
        }

        [Test]
        public void Short_name_less_than_specified_length_does_not_throw_any_validation_errors()
        {
            var target = new ShortStringTarget { Name = "Jeremy" };
            Validator.ValidateObject(target).AllMessages.Length.ShouldEqual(0);
        }
    }

    public class ShortStringTarget 
    {
        [MinimumStringLength(6)]
        public string Name { get; set; }
        
        public string LastName { get; set; }

        public int Age { get; set; }
    }
}