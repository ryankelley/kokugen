using System;
using Kokugen.Core;
using Kokugen.Core.Persistence;
using NHibernate;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace ProjectTimeTracking
{
    public class Bootstrapper : IBootstrapper
    {
        public void BootstrapStructureMap()
        {
            ObjectFactory.Initialize(x =>
                                         {
                                             x.AddRegistry(new KokugenCoreRegistry());
                                             x.AddRegistry(new TimeTrackerRegistry());
                                         });

            //ObjectFactory.AssertConfigurationIsValid();
        }

        public static void Bootstrap()
        {
            new Bootstrapper().BootstrapStructureMap();
        }
    }

    public class TimeTrackerRegistry : Registry
    {
        public TimeTrackerRegistry()
        {
            Scan(x =>
                     {
                         x.TheCallingAssembly();
                         x.WithDefaultConventions();
                     });

            

            
        }
    }
}