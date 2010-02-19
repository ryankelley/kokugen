using System.Data;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;

namespace Kokugen.Core.Persistence
{
    public class NHibernatesessionSource : ISessionSource
    {
        private readonly DatabaseSettings _databaseSettings;
        private readonly object _factorySyncRoot = new object();
        private Configuration _configuration;
        private ISessionFactory _sessionFactory;

        public NHibernatesessionSource(DatabaseSettings databaseSettings)
        {
            if(_sessionFactory != null) return;

            lock(_factorySyncRoot)
            {
                if(_sessionFactory != null) return;

                _databaseSettings = databaseSettings;
                _configuration = AssembleConfiguration(null);
                _sessionFactory = _configuration.BuildSessionFactory();
            }
        }

        public Configuration AssembleConfiguration(string mappingExportPath)
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2005
                              .ConnectionString(c => c.FromConnectionStringWithKey("KokugenData"))
                              .AdoNetBatchSize(1)
#if DEBUG
                              .ShowSql()
                              .Raw("generate_statistics", "true")
#endif
                              .UseOuterJoin()
                              //.Cache(cache =>
                              //           {
                              //               cache.UseQueryCache();
                              //               cache.ProviderClass("NHibernate.Caches.SysCache.SysCacheProvider, NHibernate.Caches.SysCache");
                              //           })
                )
                .Mappings(x =>
                              {
                                  x.AutoMappings.Add(new AutoPersistenceModelGenerator().Generate());
                                  //x.FluentMappings.AddFromAssemblyOf<RegionMap>();
                                  //x.AutoMappings.ExportTo("d:\\code\\coachesaid3\\MappingFiles");
                              })
                .ExposeConfiguration(config => config.Properties.Add("prepare_sql", "true"))
                .BuildConfiguration();
        }

        public ISession CreateSession()
        {
            return _sessionFactory.OpenSession();
        }

        public void BuildSchema()
        {
            ISession session = CreateSession();
            IDbConnection connection = session.Connection;

            Dialect dialect = Dialect.GetDialect(_databaseSettings.GetProperties());
            string[] drops = _configuration.GenerateDropSchemaScript(dialect);
            executeScripts(drops, connection);

            string[] scripts = _configuration.GenerateSchemaCreationScript(dialect);

            executeScripts(scripts, connection);
        }

        private static void executeScripts(string[] scripts, IDbConnection connection)
        {
            foreach (var script in scripts)
            {
                IDbCommand command = connection.CreateCommand();
                command.CommandText = script;
                command.ExecuteNonQuery();
            }
        }
    }
}