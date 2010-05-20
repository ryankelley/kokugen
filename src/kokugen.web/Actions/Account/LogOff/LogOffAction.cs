using FubuMVC.Core.Continuations;
using FubuMVC.Core.Security;
using FubuMVC.Core.Urls;
using Kokugen.Web.Actions.Home;

namespace Kokugen.Web.Actions.Account.LogOff
{
    public class LogOffAction
    {
        private readonly IAuthenticationContext _authContext;
        private readonly IUrlRegistry _urlRegistry;

        public LogOffAction(IAuthenticationContext authContext, IUrlRegistry urlRegistry)
        {
            _authContext = authContext;
            _urlRegistry = urlRegistry;
        }

        public FubuContinuation Execute(LogOffRequest input)
        {
            _authContext.SignOut();
            return FubuContinuation.RedirectTo(_urlRegistry.UrlFor<IndexAction>(x => x.Query()));
        }
    }
    public class LogOffRequest
    {
    }
}