using FluentNHibernate;
using FubuMVC.Core.Configuration;
using Kokugen.Core.Persistence;
using NHibernate;
using StructureMap.Configuration.DSL;

namespace Kokugen.Tests.SchemaCreation
{
    public class SchemaRegistry : Registry
    {
        public SchemaRegistry()
        {
            SetupNHibernate();
        }

        private void SetupNHibernate()
        {
            ForSingletonOf<ISessionSource>().Use<NHibernatesessionSource>();

            For<ITransactionBoundary>().Use<NHibernateTransactionBoundary>();
            For<ISession>().Use(c =>
            {
                var trans = ((NHibernateTransactionBoundary)c.GetInstance<ITransactionBoundary>());
                return trans.Session;
            });

            For<ITransactionBoundary>().Use<NHibernateTransactionBoundary>();
            For<DatabaseSettings>().Use(c =>
            {
                var settingsProvider = c.GetInstance<ISettingsProvider>();
                return settingsProvider.SettingsFor<DatabaseSettings>();
            });
        }
    }
}