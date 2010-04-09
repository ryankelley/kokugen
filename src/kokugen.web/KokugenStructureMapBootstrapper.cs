using System;
using System.Web.Routing;
using System.Web.Security;
using AutoMapper;
using FubuCore;
using FubuMVC.Core;
using FubuMVC.Core.Runtime;
using FubuMVC.StructureMap;
using Kokugen.Core;
using Kokugen.Core.Domain;
using Kokugen.Core.Persistence;
using Kokugen.Core.Services;
using Kokugen.Web.Actions.Board;
using Kokugen.Web.Actions.DTO;
using Kokugen.Web.Behaviors;
using Kokugen.Web.Conventions;
using StructureMap;
using StructureMap.Configuration.DSL;

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

            //ObjectFactory.Container.StartStartables();
            ConfigureAutoMapper();
        }

        private void ConfigureAutoMapper()
        {
            Mapper.CreateMap<Card, CardViewDTO>()
                .ForMember(a => a.Status, b=> b.MapFrom(c => c.Status.DisplayName));
            Mapper.CreateMap<BoardColumn, BoardColumnDTO>()
                .ForMember(a => a.Limit, b=> b.UseValue(0));
            Mapper.CreateMap<CustomBoardColumn, BoardColumnDTO>()
                .ForMember(a => a.Limit, b=> b.NullSubstitute(0));
            Mapper.CreateMap<TimeRecord, TimeRecordDTO>()
                .ForMember(a => a.User, b => b.NullSubstitute(null));


            Mapper.AssertConfigurationIsValid();
        }
    }

    public class KokugenWebRegistry : Registry
    {
        public KokugenWebRegistry()
        {
            Scan(x =>
                     {
                         x.TheCallingAssembly();
                         x.WithDefaultConventions();

                         x.AddAllTypesOf<IStartable>();
                     }
                );
        }
    }
}