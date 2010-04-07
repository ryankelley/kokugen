using FubuMVC.StructureMap;
using Kokugen.Core.Membership.Settings;
using NUnit.Framework;
using StructureMap;

namespace Kokugen.Tests.Core.Membership.Settings
{
    [TestFixture]
    public class MembershipSettingsTester 
    {
        private IContainer _container;
        private IMembershipSettingsProvider _provider;

        [SetUp]
        public void setup()
        {
            _container = StructureMapContainerFacility.GetBasicFubuContainer();
            _container.Configure(x => x.For<IMembershipSettingsProvider>()
                .Use<AspNetMembershipSettingsProvider>());

            _provider = _container.GetInstance<IMembershipSettingsProvider>();
        }

        [Test]
        public void smoke_test()
        {
            var loginSettings = _provider.SettingsFor<LoginSettings>();
            loginSettings.MaxInvalidPasswordAttempts.ShouldEqual(5);
            loginSettings.PasswordAttemptWindow.ShouldEqual(10);
        }
    }
}