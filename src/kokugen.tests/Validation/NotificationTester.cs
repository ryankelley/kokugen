using Kokugen.Core.Validation;
using NUnit.Framework;

namespace Kokugen.Tests.Validation
{
    [TestFixture]
    public class NotificationTester
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
        }

        #endregion

        [Test]
        public void IsTopLevelValid()
        {
            var notification = new Notification();
            var child = new Notification();
            notification.AddChild("something", child);
            child.RegisterMessage("f1", "bad", Severity.Error);

            notification.IsTopLevelValid().ShouldBeTrue();

            notification.RegisterMessage("f2", "not bad", Severity.Warning);
            notification.IsTopLevelValid().ShouldBeTrue();

            notification.RegisterMessage("f2", "really bad", Severity.Error);
            notification.IsTopLevelValid().ShouldBeFalse();

        }

        [Test]
        public void AliasFields()
        {
            var notification = new Notification();
            notification.RegisterMessage("Field1", "aaa[Field1]aaa", Severity.Error);

            notification.AliasFieldInMessages("Field1", "The First Field");
            notification.AllMessages[0].Message.ShouldEqual("aaaThe First Fieldaaa");
        }

        [Test]
        public void GetChild()
        {
            var notification = new Notification();
            var child = new Notification();
            notification.AddChild("something", child);

            Assert.AreSame(child, notification.GetChild("something"));
        }

        [Test]
        public void GetChildReturnsValidIfNoChild()
        {
            var notification = new Notification();
            var child = notification.GetChild("anything");

            child.IsValid().ShouldBeTrue();
        }

        [Test]
        public void IgnoreDuplicates()
        {
            var notification = new Notification();
            notification.RegisterMessage("a", "b", Severity.Error);
            notification.RegisterMessage("a", "b", Severity.Error);
            notification.RegisterMessage("a", "b", Severity.Error);
            notification.RegisterMessage("a", "b", Severity.Error);
            notification.RegisterMessage("a", "b", Severity.Error);

            notification.AllMessages.Length.ShouldEqual(1);
        }

        [Test]
        public void IsValid()
        {
            var notification = new Notification();
            notification.IsValid().ShouldBeTrue();

            notification.RegisterMessage("some field", "some message", Severity.Error);
            notification.IsValid().ShouldBeFalse();
        }

        [Test]
        public void NotificationMessage_Has_An_Error_Severity_By_Default()
        {
            var message = new NotificationMessage("something", "else");
            message.Severity.ShouldEqual(Severity.Error);
        }


        [Test]
        public void RegisterMessageAndRecall()
        {
            string theFirstField = "field1";
            string theSecondField = "field2";

            var notification = new Notification();
            notification.RegisterMessage(theFirstField, "message1", Severity.Error);
            notification.RegisterMessage(theFirstField, "message2", Severity.Error);
            notification.RegisterMessage(theFirstField, "message3", Severity.Error);
            notification.RegisterMessage(theSecondField, "message4", Severity.Error);
            notification.RegisterMessage(theSecondField, "message5", Severity.Error);

            notification.GetMessages(theFirstField).ShouldEqual(
                new[]
                    {
                        new NotificationMessage(theFirstField, "message1"),
                        new NotificationMessage(theFirstField, "message2"),
                        new NotificationMessage(theFirstField, "message3")
                    });

            notification.GetMessages(theSecondField).ShouldEqual(
                new[]
                    {
                        new NotificationMessage(theSecondField, "message4"),
                        new NotificationMessage(theSecondField, "message5")
                    });
        }

        [Test]
        public void UseChildInIsValidDetermination()
        {
            var notification = new Notification();
            notification.IsValid().ShouldBeTrue();

            var child = new Notification();
            notification.AddChild("something", child);

            notification.IsValid().ShouldBeTrue();

            child.RegisterMessage("field1", "bad value!", Severity.Error);
            notification.IsValid().ShouldBeFalse();
        }

        [Test]
        public void UseChildInIsValidForFieldDetermination()
        {
            var notification = new Notification();
            notification.IsValid("field1").ShouldBeTrue();

            var child = new Notification();
            notification.AddChild("something", child);

            notification.IsValid("field1").ShouldBeTrue();

            child.RegisterMessage("field1", "bad value!", Severity.Error);

            notification.IsValid("field1").ShouldBeFalse();
            notification.IsValid("different").ShouldBeTrue();
        }

        [Test]
        public void Get_field_bag()
        {
            Notification notification = new Notification();
            var message1 = notification.RegisterMessage("f1", "", Severity.Error);
            var message2 = notification.RegisterMessage("f2", "", Severity.Error);
            var message3 = notification.RegisterMessage("f1", "", Severity.Error);
            var message4 = notification.RegisterMessage("f3", "", Severity.Error);
            var message5 = notification.RegisterMessage("f3", "", Severity.Error);
            var message6 = notification.RegisterMessage("f2", "", Severity.Error);

            notification.MessagesFor("f1").Contains(message1).ShouldBeTrue();
            notification.MessagesFor("f1").Contains(message3).ShouldBeTrue();
            notification.MessagesFor("f2").Contains(message2).ShouldBeTrue();
            notification.MessagesFor("f2").Contains(message6).ShouldBeTrue();
            notification.MessagesFor("f3").Contains(message4).ShouldBeTrue();
            notification.MessagesFor("f3").Contains(message5).ShouldBeTrue();
            
            
        }

        [Test]
        public void FlattenA3DeepNotification()
        {
            Notification notification = new  Notification();
            notification.RegisterMessage("f1", "hello", Severity.Error);

            Notification child = new Notification();
            child.RegisterMessage("f3", "something", Severity.Warning);

            notification.AddChild("f2", child);

            Notification grandchild = new Notification();
            grandchild.RegisterMessage("f4", "bad", Severity.Error);

            child.AddChild("f5", grandchild);

            Notification flat = notification.Flatten();
            flat.AllMessages.Length.ShouldEqual(3);
        }
    }
}