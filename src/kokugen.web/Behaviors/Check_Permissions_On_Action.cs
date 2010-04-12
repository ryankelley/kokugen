using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.Security;
using FubuMVC.Core.Urls;
using Kokugen.Core.Membership;

namespace Kokugen.Web.Behaviors
{
    public class MustBeAuthorizedBehavior : BasicBehavior
    {
        private readonly ISecurityContext _securityContext;
        private readonly IFubuRequest _request;
        private readonly ISecurityRegistry _securityRegistry;

        public MustBeAuthorizedBehavior(ISecurityContext securityContext, IOutputWriter writer, IUrlRegistry urls, IFubuRequest request, ISecurityRegistry securityRegistry)
            : base(PartialBehavior.Ignored)
        {
            _securityContext = securityContext;
            _request = request;
            _securityRegistry = securityRegistry;
        }

        protected override FubuMVC.Core.DoNext performInvoke()
        {
            if(!_securityContext.IsAuthenticated())
            {
                // The User is not logged in, better send back to login form
            }

            return DoNext.Continue;
        }
    }
}