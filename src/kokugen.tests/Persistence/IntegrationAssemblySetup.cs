using FluentNHibernate;
using Kokugen.Core;
using Kokugen.Core.Persistence;
using NUnit.Framework;
using StructureMap;

namespace Kokugen.Tests.Persistence
{
    [SetUpFixture]
    public class IntegrationAssemblySetup
    {
        private static ISessionSource _sessionSource;

        public static ISessionSource SessionSource
        {
            get { return _sessionSource; }
        }

        [SetUp]
        public void Run_Before_Any_Tests()
        {
            ObjectFactory.Initialize(x =>
                                         {
                                             //x.AddRegistry(new KokugenCoreRegistry());
                                             x.AddRegistry(new PersistenceTestRegistry());

                                         });

            ObjectFactory.AssertConfigurationIsValid();

            _sessionSource = ObjectFactory.GetInstance<ISessionSource>();
            _sessionSource.BuildSchema();
        }

        [TearDown]
        public void Run_after_all_tests()
        {
            //var transaction = (NHibernateTransactionBoundary)ObjectFactory.GetInstance<ITransactionBoundary>();
            //transaction.Commit();
        }

    }
}