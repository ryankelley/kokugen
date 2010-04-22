using System;
using Kokugen.Core.Membership.Settings;
using NUnit.Framework;

namespace Kokugen.Tests.Core.Membership.Services
{
    [TestFixture]
    public class PasswordValidatorTester : InteractionContext<PasswordValidator>
    {
        [Test]
        public void password_should__false_one_validate_non_alpha_numberic_limit_of_1()
        {
            Services.Inject(new PasswordSettings()
            {
                MinRequiredNonAlphanumericCharacters = 1,
            });

            string password = "weakasspassword";
            ClassUnderTest.ValidatePassword(password).ShouldBeFalse();
        }

        [Test]
        public void password_should_be_true_on_validate_non_alpha_numberic_limit_of_1()
        {
            Services.Inject(new PasswordSettings()
            {
                MinRequiredNonAlphanumericCharacters = 1,
            });

            string password = "kick@ssP@ssword";
            ClassUnderTest.ValidatePassword(password).ShouldBeTrue();
        }

        [Test]
        public void password_should_be_true_on_validate_non_alpha_numberic_limit_of_0()
        {
            Services.Inject(new PasswordSettings()
            {
                MinRequiredNonAlphanumericCharacters = 0,
            });

            string password = "weakasspassword";
            ClassUnderTest.ValidatePassword(password).ShouldBeTrue();
        }

        [Test]
        public void password_should_validate_min_length_false()
        {
            ClassUnderTest.ValidatePassword("weak@ss").ShouldBeFalse();
        }

        [Test]
        public void password_should_validate_min_length_true()
        {
            ClassUnderTest.ValidatePassword("weak@ssPass").ShouldBeTrue();
        }

        [Test]
        public void password_should_also_check_custom_regex_fail()
        {
            Services.Inject(new PasswordSettings()
                                {
                                    PasswordStrengthRegularExpression = "^(?=.{16,})"
                                });
            ClassUnderTest.ValidatePassword("cust0m$trength").ShouldBeFalse();
        }
        [Test]
        public void password_should_also_check_custom_regex_pass()
        {
            Services.Inject(new PasswordSettings()
            {
                PasswordStrengthRegularExpression = "^(?=.{14,})"
            });
            ClassUnderTest.ValidatePassword("cust0m$trength").ShouldBeTrue();
        }

    }
}