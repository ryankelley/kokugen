using System;
using System.Collections.Generic;
using System.ComponentModel;
using Kokugen.Core.Validation;
using NUnit.Framework;

namespace Kokugen.Tests.Validation
{
    [TestFixture]
    public class ValidatorTester
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
        }

        #endregion

        [Test]
        public void FindAllValidationAttributes()
        {
            List<ValidationAttribute> atts = Validator.FindAttributes(typeof (ValidationTarget));
            atts.Sort();

            var expected = new List<ValidationAttribute>
                               {
                                   new StubValidation("age1", "Age"),
                                   new StubValidation("age2", "Age"),
                                   new StubValidation("name1", "Name")
                               };

            atts.ShouldEqual(expected);
        }

        [Test]
        public void HierarchicalValidation()
        {
            var target = new HierarchicalValidationTarget
                             {
                                 Name = "Me!",
                                 Child = new ValidatedTarget()
                             };

            Validator.ValidateObject(target).GetChild("Child").AllMessages
                .ShouldEqual(
                Validator.ValidateObject(target.Child).AllMessages);
        }

        [Test]
        public void HierarchicalValidation_does_not_blow_up_with_missing_child()
        {
            var target = new HierarchicalValidationTarget
                             {
                                 Name = "Me!"
                             };

            Validator.ValidateObject(target).GetChild("Child");            
        }

        [Test]
        public void Validate()
        {
            var notification = Validator.ValidateObject(new ValidationTarget());

            var expected = new[]
                               {
                                   new NotificationMessage("Name", "name1"),
                                   new NotificationMessage("Age", "age2"),
                                   new NotificationMessage("Age", "age1")
                                   
                                   
                               };

            notification.AllMessages.Length.ShouldEqual(expected.Length);
        }

        [Test]
        public void ValidateASecondTime()
        {
            var notification1 = Validator.ValidateObject(new ValidationTarget());
            var notification2 = Validator.ValidateObject(new ValidationTarget());
            var notification3 = Validator.ValidateObject(new ValidationTarget());
            var notification4 = Validator.ValidateObject(new ValidationTarget());
            var notification5 = Validator.ValidateObject(new ValidationTarget());

            var expected = new[]
                               {
                                   new NotificationMessage("Name", "name1"),
                                   new NotificationMessage("Age", "age2"),
                                   new NotificationMessage("Age", "age1")
                               };

            notification1.AllMessages.Length.ShouldEqual(expected.Length);
            notification2.AllMessages.Length.ShouldEqual(expected.Length);
            notification3.AllMessages.Length.ShouldEqual(expected.Length);
            notification4.AllMessages.Length.ShouldEqual(expected.Length);
            notification5.AllMessages.Length.ShouldEqual(expected.Length);
        }

        [Test]
        public void ValidateASingleField()
        {
            NotificationMessage[] messages = Validator.ValidateField(new ValidationTarget(), "Age");
            Array.Sort(messages);

            var expected = new[]
                               {
                                   new NotificationMessage("Age", "age1"),
                                   new NotificationMessage("Age", "age2")
                               };

            messages.ShouldEqual(expected);
        }

        [Test]
        public void ValidateAValidatedObject()
        {
            var target = new ValidatedTarget();
            var notification = Validator.ValidateObject(target);
            target.AssertWasValidated(notification);
        }
    }

    internal class ValidatedTarget : IValidated
    {
        private bool _wasCalled;

        [Browsable(false), StubValidation("name1")]
        public string Name { get; set; }

        #region IValidated Members

        public void Validate(Notification notification)
        {
            _wasCalled = true;
            notification.RegisterMessage("some field", "some message", Severity.Error);
        }

        #endregion

        public void AssertWasValidated(INotification notification)
        {
            Assert.IsTrue(_wasCalled);

            var expected = new[]
                               {
                                   new NotificationMessage("some field", "some message"),
                                   new NotificationMessage("Name", "name1")
                                   
                               };

            Assert.AreEqual(expected, notification.AllMessages);
        }
    }

    internal class HierarchicalValidationTarget
    {
        [ValidatedChild]
        public ValidatedTarget Child { get; set; }

        [StubValidation("parent")]
        public string Name { get; set; }
    }

    internal class ValidationTarget
    {
        [Browsable(false), StubValidation("name1")]
        public string Name { get; set; }

        [StubValidation("age1"), StubValidation("age2")]
        public int Age { get; set; }

        [Browsable(true)]
        public DateTime? CreationTime { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    internal class StubValidation : ValidationAttribute, IComparable
    {
        private readonly string _field;
        private readonly string _message;

        public StubValidation(string message)
        {
            _message = message;
        }


        public StubValidation(string message, string field)
        {
            _message = message;
            _field = field;
        }


        public string Field
        {
            get
            {
                if (Property != null)
                {
                    return Property.Name;
                }

                return _field;
            }
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            return _message.CompareTo(((StubValidation) obj)._message);
        }

        #endregion

        protected override void validate(object target, object rawValue, INotification notification)
        {
            logMessage(notification, _message);
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            var stubValidation = obj as StubValidation;
            if (stubValidation == null) return false;
            return Equals(_message, stubValidation._message) && Equals(Field, stubValidation.Field);
        }

        public override int GetHashCode()
        {
            return (_message != null ? _message.GetHashCode() : 0) + 29*(_field != null ? _field.GetHashCode() : 0);
        }


        public override string ToString()
        {
            return new NotificationMessage(Field, _message).ToString();
        }
    }
}