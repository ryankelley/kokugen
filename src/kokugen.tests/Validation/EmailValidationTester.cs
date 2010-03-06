using Kokugen.Core.Validation;
using NUnit.Framework;

namespace Kokugen.Tests.Validation
{
    [TestFixture]
    public class EmailValidationTester
    {

        [Test]
        public void Should_validate_with_valid_email_address()
        {
            ValidateEmail("john@thisoneplace.com").IsValid().ShouldBeTrue();
        }

        [Test]
        public void Should_not_validate_if_there_is_no_at_symbol()
        {
            ValidateEmail("johnthisoneplace.com").IsValid().ShouldBeFalse();
        }

        [Test]
        public void Should_not_validate_if_there_is_no_tld_at_end()
        {
            ValidateEmail("john@thisoneplacenet").IsValid().ShouldBeFalse();
        }

        [Test]
        public void Should_validate_if_there_is_net_at_end()
        {
            ValidateEmail("john@thisoneplace.net").IsValid().ShouldBeTrue();
        }

        [Test]
        public void Should_validate_if_there_is_edu_at_end()
        {
            ValidateEmail("john@thisoneplace.edu").IsValid().ShouldBeTrue();
        }

        [Test]
        public void Should_validate_if_there_is_two_letter_tld()
        {
            ValidateEmail("john@thisoneplace.cr").IsValid().ShouldBeTrue();
        }

        [Test]
        public void Should_validate_if_there_is_multipart_tld()
        {
            ValidateEmail("john@thisoneplace.co.uk").IsValid().ShouldBeTrue();
        }

        [Test]
        public void Should_not_validate_if_there_is_no_text_before_the_at_symbol()
        {
            ValidateEmail("@thisoneplace.net").IsValid().ShouldBeFalse();
        }

        [Test]
        public void Should_validate_if_there_is_dot_in_the_local_part()
        {
            ValidateEmail("john.test@thisoneplace.net").IsValid().ShouldBeTrue();
        }

        [Test]
        public void Should_not_validate_if_there_are_two_periods_in_a_row()
        {
            ValidateEmail("john..test@test.com").IsValid().ShouldBeFalse();
        }

        private INotification ValidateEmail(string emailAddress)
        {
            var expected = new EmailObject { EmailAddress = emailAddress };

            return Validator.ValidateObject(expected);
        }


        private class EmailObject
        {
            [ValidEmail]
            public virtual string EmailAddress { get; set; }
        }
    }
}