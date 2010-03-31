using FluentNHibernate;
using FubuMVC.Core.Configuration;
using Kokugen.Core.Persistence;
using NHibernate;
using StructureMap.Configuration.DSL;

namespace Kokugen.Tests
{
    public class PersistenceTestRegistry : Registry
    {
        public PersistenceTestRegistry()
        {
            //ForSingletonOf<ISessionSource>().Use<NHibernatesessionSource>();
            For<ISessionSource>().Singleton()
                .TheDefault.Is.ConstructedBy(ctx =>
                    ctx.GetInstance<NHibernatesessionSource>()
                    .CreateSessionSource());

            For<ISession>().Use(c => c.GetInstance<ISessionSource>().CreateSession());
            For<DatabaseSettings>().Use(c =>
            {
                var settingsProvider = c.GetInstance<ISettingsProvider>();
                return settingsProvider.SettingsFor<DatabaseSettings>();
            });
        }
    }
}