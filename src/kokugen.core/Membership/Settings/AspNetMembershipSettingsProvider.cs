using System;
using System.Web.Security;
using FubuCore.Binding;
using FubuMVC.Core.Configuration;
using Microsoft.Practices.ServiceLocation;

namespace Kokugen.Core.Membership.Settings
{
    public class AspNetMembershipSettingsProvider : IMembershipSettingsProvider
    {
        private readonly IObjectResolver _resolver;
        private readonly IServiceLocator _locator;

        public AspNetMembershipSettingsProvider(IObjectResolver resolver, IServiceLocator locator)
        {
            _resolver = resolver;
            _locator = locator;
        }

        public T SettingsFor<T>() where T : class, new()
        {
            Type settingsType = typeof(T);

            object value = SettingsFor(settingsType);

            return (T)value;
        }

        public object SettingsFor(Type settingsType)
        {
            IBindingContext context =
                new BindingContext(new MembershipSettingsDictionary(
                    System.Web.Security.Membership.Provider), _locator);

            BindResult result = _resolver.BindModel(settingsType, context);

            result.AssertNoProblems(settingsType);

            return result.Value;
        }
    }

    public interface IMembershipSettingsProvider : ISettingsProvider {}
}