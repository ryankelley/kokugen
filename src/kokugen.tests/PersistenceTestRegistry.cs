using FluentNHibernate;
using Kokugen.Core.Persistence;
using NHibernate;
using StructureMap.Configuration.DSL;

namespace Kokugen.Tests
{
    public class PersistenceTestRegistry : Registry
    {
        public PersistenceTestRegistry()
        {
            ForSingletonOf<ISessionSource>().Use(new NHibernatesessionSource().CreateSessionSource());

            //For<ISessionSource>().Singleton()
            //    .TheDefault.Is.ConstructedBy(ctx =>
            //        ctx.GetInstance<NHibernatesessionSource>()
            //        .CreateSessionSource());

            For<ISession>().Use(c => c.GetInstance<ISessionSource>().CreateSession());
        }
    }
}