using System.Linq;
using System.Web.Routing;
using AutoMapper;
using FubuCore;
using FubuMVC.Core;
using FubuMVC.StructureMap;
using Kokugen.Core;
using Kokugen.Core.Domain;
using Kokugen.Core.Membership;
using Kokugen.Core.Services;
using Kokugen.Web.Actions.Board;
using Kokugen.Web.Actions.Card;
using Kokugen.Web.Actions.DTO;
using Kokugen.Web.Actions.Project.Manage.Users;
using Kokugen.Web.Actions.TimeRecord.WidgetLog;
using Kokugen.Web.Behaviors;
using StructureMap;

namespace Kokugen.Web
{
    public class KokugenStructureMapBootstrapper
    {
        private readonly RouteCollection _routes;

        private KokugenStructureMapBootstrapper(RouteCollection routes)
        {
            _routes = routes;
        }

        public static void Bootstrap(RouteCollection routes, FubuRegistry fubuRegistry)
        {
            new KokugenStructureMapBootstrapper(routes).BootstrapStructureMap(fubuRegistry);
        }

        private void BootstrapStructureMap(FubuRegistry fubuRegistry)
        {
            UrlContext.Reset();

            ObjectFactory.Initialize(x =>
                                         {
                                             x.AddRegistry(new KokugenCoreRegistry());
                                             x.AddRegistry(new KokugenWebRegistry());
                                         });

            var fubuBootstrapper = new StructureMapBootstrapper(ObjectFactory.Container, fubuRegistry);
            fubuBootstrapper.Builder = (c, args, id) =>
                                           {
                                               return new TransactionalContainerBehavior(c, args, id);
                                           };
            fubuBootstrapper.Bootstrap(_routes);

            ObjectFactory.Container.StartStartables();

            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            
            
        }

    }
}