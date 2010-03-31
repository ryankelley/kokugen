using System.Security.Principal;
using FluentNHibernate;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Configuration;
using FubuMVC.Core.Security;
using Kokugen.Core.Persistence;
using Kokugen.Core.Security;
using Kokugen.Core.Services;
using NHibernate;
using StructureMap.Configuration.DSL;

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
                     });
        }

        private void setupNHibernate()
        {
            ForSingletonOf<ISessionSource>().Use<NHibernatesessionSource>();
            For<IPrincipalFactory>().Use<KokugenPrincipalFactory>();
            For<IPrincipal>().Use<KokugenPrincipal>();


            For<ISession>().Use(c =>
                                    {
                                        var transaction = (NHibernateTransactionBoundary) c.GetInstance<ITransactionBoundary>();
                                        return transaction.Session;
                                    });

            //For<IUniqueidentifierService>()
            //    .Use<NHibernateUniqueIdentifierService>();

            For<ITransactionBoundary>().Use<NHibernateTransactionBoundary>();

            For<DatabaseSettings>().Use(c =>
            {
                var settingsProvider = c.GetInstance<ISettingsProvider>();
                return settingsProvider.SettingsFor<DatabaseSettings>();
            });
        }
    }
}