using System;
using System.Web.Security;
using FubuMVC.StructureMap;
using Kokugen.Core;
using Kokugen.Core.Membership.Abstractions.ASP_NET;
using Kokugen.Core.Membership.Settings;
using NUnit.Framework;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Kokugen.Tests.Core.Membership.Settings
{
    [TestFixture]
    public class MembershipSettingsTester 
    {
        private IContainer _container;
        private MembershipProvider _provider;

        [SetUp]
        public void setup()
        {
            _container = StructureMapContainerFacility.GetBasicFubuContainer();
            _container.Configure(x => {
                                          x.For<IMembershipSettingsProvider>().Use<AspNetMembershipSettingsProvider>();
                                          x.Scan(scan =>
                                                     {
                                                         scan.AssemblyContainingType<LoginSettings>();
                                                         scan.IncludeNamespace(typeof(LoginSettings).Namespace);
                                                         scan.Convention<MembershipSettingsConvention>();
                                                     });
            });

            _provider = System.Web.Security.Membership.Provider;


        }

        [Test]
        public void login_settings_smoke_test()
        {
            var loginSettings = _container.GetInstance<LoginSettings>();
            loginSettings.MaxInvalidPasswordAttempts.ShouldEqual(_provider.MaxInvalidPasswordAttempts);
            loginSettings.PasswordAttemptWindow.ShouldEqual(_provider.PasswordAttemptWindow);
        }

        [Test]
        public void password_settings_smoke_test()
        {
            var settings = _container.GetInstance<PasswordSettings>();
            settings.MinRequiredNonAlphanumericCharacters.ShouldEqual(_provider.MinRequiredNonAlphanumericCharacters);
            settings.MinRequiredPasswordLength.ShouldEqual(_provider.MinRequiredPasswordLength);
            settings.PasswordFormat.ShouldEqual(_provider.PasswordFormat);
            settings.PasswordStrengthRegularExpression.ShouldEqual(_provider.PasswordStrengthRegularExpression);
        }

        [Test]
        public void password_retrieval_settings_smoke_test()
        {
            var settings = _container.GetInstance<PasswordResetRetrievalSettings>();
            settings.EnablePasswordReset.ShouldEqual(_provider.EnablePasswordReset);
            settings.EnablePasswordRetrieval.ShouldEqual(_provider.EnablePasswordRetrieval);
            settings.RequiresQuestionAndAnswer.ShouldEqual(_provider.RequiresQuestionAndAnswer);
        }

        [Test]
        public void registration_settings_smoke_test()
        {
            var settings = _container.GetInstance<RegistrationSettings>();
            settings.RequiresUniqueEmail.ShouldEqual(_provider.RequiresUniqueEmail);
        }
    }

  
}