using System;
using System.Net.Mail;
using System.Security.Principal;
using System.Web.Security;
using FluentNHibernate;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Configuration;
using FubuMVC.Core.Security;
using Kokugen.Core.Events;
using Kokugen.Core.Membership.Security;
using Kokugen.Core.Membership.Services;
using Kokugen.Core.Membership.Settings;
using Kokugen.Core.Persistence;
using Kokugen.Core.Services;
using NHibernate;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Kokugen.Core
{
    public class KokugenCoreRegistry : Registry
    {
        public KokugenCoreRegistry()
        {
            setupNHibernate();

            Scan(x =>
                     {
                         x.TheCallingAssembly();
                         x.WithDefaultConventions();
                         x.Convention<SettingsConvention>();
                     });

            setupEmail();
            registerEventAggregator();
        }

        private void setupEmail()
        {
            For<IEmailService>()
                .Use(c =>
                         {
                             var settingsProvider = c.GetInstance<ISettingsProvider>();
                             var emailSettings = settingsProvider.SettingsFor<EmailSettings>();
                             return new EmailService(new SmtpClient(emailSettings.Host, emailSettings.Port),
                                                     emailSettings.User, emailSettings.Password,
                                                     emailSettings.AuthorizationRequired);
                         });
        }

        private void setupNHibernate()
        {
            ForSingletonOf<ISessionSource>().Use<NHibernatesessionSource>();
            For<IPrincipalFactory>().Use<FubuPrincipalFactory>();
            For<IPrincipal>().Use<FubuPrincipal>();


            For<ISession>().Use(c =>
                                    {
                                        var transaction = (NHibernateTransactionBoundary) c.GetInstance<ITransactionBoundary>();
                                        return transaction.Session;
                                    });

            //For<IUniqueidentifierService>()
            //    .Use<NHibernateUniqueIdentifierService>();

            For<ITransactionBoundary>().Use<NHibernateTransactionBoundary>();

            For<IConfigurationProperties>().Use(c =>
            {
                var settingsProvider = c.GetInstance<ISettingsProvider>();
                return settingsProvider.SettingsFor<DatabaseSettings>();
            });

            
        }

        private void registerEventAggregator()
        {
            For<IEventAggregator>().Singleton().Use<EventAggregator>();
            RegisterInterceptor(new EventAggregatorInterceptor());
        }
    }

    public class SettingsConvention : IRegistrationConvention
    {
        public void Process(Type type, Registry registry)
        {
            if (type.IsAbstract || type.IsInterface)
                return;
            if (type.Name.EndsWith("Settings"))
                registry.For(type).Use(x =>
                {
                    var provider = x.GetInstance<ISettingsProvider>();
                    return provider.SettingsFor(type);
                });
        }
    }
}