using Kokugen.Core.Validation;
using NUnit.Framework;

namespace Kokugen.Tests.Validation
{
    [TestFixture]
    public class ValidationRulesTester
    {


        [Test]
        public void SimpleValidation_for_ShouldBeGreaterThanZero()
        {
            ValidationRules<RuleTestClass> rules = new ValidationRules<RuleTestClass>();
            rules.IfProperty(x => x.Matches).Equals(true).Property(x => x.Age).ShouldBeGreaterThanZero();

            RuleTestClass target = new RuleTestClass { Matches = false, Age = 0 };
            rules.Validate(target).IsValid("Age").ShouldBeTrue();

            target.Matches = true;
            rules.Validate(target).IsValid("Age").ShouldBeFalse();

            target.Age = 34;
            rules.Validate(target).IsValid("Age").ShouldBeTrue();

        }

        [Test]
        public void SimpleValidation_for_Required()
        {
            ValidationRules<RuleTestClass> rules = new ValidationRules<RuleTestClass>();
            rules.IfProperty(x => x.Matches).Equals(true).Property(x => x.Name).ShouldBeRequired().WithMessage("a message");

            RuleTestClass target = new RuleTestClass { Matches = false, Name = null };
            rules.Validate(target).IsValid("Name").ShouldBeTrue();

            target.Matches = true;
            rules.Validate(target).IsValid("Name").ShouldBeFalse();

            target.Name = "Jeremy";
            rules.Validate(target).IsValid("Name").ShouldBeTrue();
        }

        [Test]
        public void Required_Validation_picks_up_message()
        {
            ValidationRules<RuleTestClass> rules = new ValidationRules<RuleTestClass>();
            rules.IfProperty(x => x.Matches).Equals(true).Property(x => x.Name).ShouldBeRequired().WithMessage("a message");

            RuleTestClass target = new RuleTestClass { Matches = true, Name = null };
            rules.Validate(target).IsValid("Name").ShouldBeFalse();
            rules.Validate(target).AllMessages[0].Message.ShouldEqual("a message");
        }
    }

    public class RuleTestClass
    {
        public int Age { get; set; }
        public string Name { get; set; }
        public bool Matches { get; set; }
    }
}