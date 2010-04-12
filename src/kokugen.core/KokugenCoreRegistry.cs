using System;
using System.Security.Principal;
using System.Web.Security;
using FluentNHibernate;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Configuration;
using FubuMVC.Core.Security;
using Kokugen.Core.Membership.Abstractions.ASP_NET;
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
            setupMembership();
            Scan(x =>
                     {
                         x.TheCallingAssembly();
                         x.WithDefaultConventions();
                     });
        }

        private void setupMembership()
        {
            For<MembershipProvider>().Use(c => System.Web.Security.Membership.Provider);
            For<RoleProvider>().Use(c => Roles.Provider);

            ForSingletonOf<IUserService>()
              .Use<AspNetMembershipProviderWrapper>();

            ForSingletonOf<IMembershipValidator>()
                .Use<AspNetMembershipProviderWrapper>();

            ForSingletonOf<IPasswordService>()
               .Use<AspNetMembershipProviderWrapper>();

            ForSingletonOf<IRolesService>()
                .Use<AspNetRoleProviderWrapper>();

            For<IMembershipSettingsProvider>().Use<AspNetMembershipSettingsProvider>();

            Scan(x =>
                     {
                         x.AssemblyContainingType<LoginSettings>();
                         x.IncludeNamespaceContainingType<LoginSettings>();
                         x.Convention<MembershipSettingsConvention>();
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


    }

    public class MembershipSettingsConvention : IRegistrationConvention
    {
        public void Process(Type type, Registry registry)
        {
            if (type.IsAbstract || type.IsInterface)
                return;
            if (type.Name.EndsWith("Settings"))
                registry.For(type).Use(x =>
                {
                    var provider = x.GetInstance<IMembershipSettingsProvider>();
                    return provider.SettingsFor(type);
                });
        }
    }
}