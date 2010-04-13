using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Security;
using Kokugen.Core.Membership;

namespace Kokugen.Web.Behaviors
{
    public class MustBeAuthorizedBehavior : BasicBehavior
    {
        private readonly ActionCall _actionCall;
        private readonly ISecurityContext _securityContext;

        public MustBeAuthorizedBehavior(ActionCall actionCall,  ISecurityContext securityContext) : base(PartialBehavior.Ignored)
        {
            _actionCall = actionCall;
            _securityContext = securityContext;
        }

        protected override DoNext performInvoke()
        {
            if(SecurityProvider.HasPermissionForMethod(_actionCall.HandlerType, _actionCall.Method))
            {
                if(!_securityContext.IsAuthenticated())
                {
                    // Redirect to login
                    return DoNext.Stop;
                }

                var validPermissions = SecurityProvider.GetPermissionsForMethod(_actionCall.HandlerType, _actionCall.Method);

                foreach (var perm in validPermissions)
                {
                    if (_securityContext.CurrentUser.IsInRole(perm.DisplayName))
                        return DoNext.Continue;
                }

                // Redirect to 403
                return DoNext.Stop;
                
            }
      

            return DoNext.Continue;
        }
    }
}