using FubuMVC.Core;
using FubuMVC.Core.Security;
using FubuMVC.Core.Urls;
using FubuMVC.Core.View;
using Kokugen.Core.Membership.Security;
using Kokugen.Web.Actions.Account.Login;
using Kokugen.Web.Actions.Account.LogOff;

namespace Kokugen.Web.Actions.Account.LoginStatus
{
    public class LoginStatusAction
    {
        private readonly ISecurityContext _securityContext;
        private readonly IUrlRegistry _urlRegistry;

        public LoginStatusAction(ISecurityContext securityContext,
                                 IUrlRegistry urlRegistry)
        {
            _securityContext = securityContext;
            _urlRegistry = urlRegistry;
        }

        [FubuPartial]
        public LoginStatusModel Execute(LoginStatusModel input)
        {
            if (_securityContext.IsAuthenticated())
            {
                var user = (_securityContext.CurrentUser as FubuPrincipal).User;
                return new LoginStatusModel()
                           {
                               IsAuthenticated = true,
                               UserName = _securityContext.CurrentUser.Identity.Name,
                               EmailAddress = user != null ? user.Email : "",
                    RawUrl = _urlRegistry.UrlFor(new LogOffRequest())
                };

            }

            return new LoginStatusModel()
            {
                IsAuthenticated = false,
                UserName = "",
                EmailAddress = "",
                RawUrl = _urlRegistry.UrlFor(new LoginFormModel())
            };

        }
    }

    public class LoginStatusModel
    {
        public string RawUrl { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public bool IsAuthenticated { get; set; }
    }

    public class LoginStatus : FubuPage<LoginStatusModel>
    {
    }
}