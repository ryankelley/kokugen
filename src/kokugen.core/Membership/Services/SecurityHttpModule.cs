using System;
using System.Web;
using Kokugen.Core.Services;
using StructureMap;

namespace Kokugen.Core.Membership.Services
{
    public class SecurityHttpModule : IHttpModule
    {
        public void Init(HttpApplication application)
        {
            application.AuthenticateRequest += context_AuthenticateRequest;
        }
        public void Dispose()
        {
        }
        private static void context_AuthenticateRequest(object sender, EventArgs e)
        {
            
            ObjectFactory.Container.ExecuteInTransaction(c =>
                                                             {
                                                                 var authSvc = c.GetInstance<IAuthenticationService>();
                                                                 authSvc.AfterUserAuthenticated();
                                                             });
            
        }

    }
}