using System.Web;
using System.Web.Routing;
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
        private readonly ISecurityProvider _securityProvider;
        private readonly ISecurityContext _securityContext;

        public MustBeAuthorizedBehavior(ISecurityProvider securityProvider, ISecurityContext securityContext) : base(PartialBehavior.Ignored)
        {
            _securityProvider = securityProvider;
            _securityContext = securityContext;
        }

        protected override DoNext performInvoke()
        {
            //if(_securityProvider.HasPermissionForUrl(_requestBase.RawUrl))
      

            return DoNext.Continue;
        }
    }
}