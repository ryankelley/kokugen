using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.Security;
using FubuMVC.Core.Urls;
using Kokugen.Core.Membership;
using Kokugen.Web.Actions.Account.Login;

namespace Kokugen.Web.Behaviors
{
    public class MustBeAuthorizedBehavior : BasicBehavior
    {
        private readonly ActionCall _actionCall;
        private readonly ISecurityContext _securityContext;
        private readonly IUrlRegistry _urls;
        private readonly IFubuRequest _request;
        private readonly IOutputWriter _writer;

        public MustBeAuthorizedBehavior(ActionCall actionCall, ISecurityContext securityContext, IUrlRegistry urls, IFubuRequest request, IOutputWriter writer)
            : base(PartialBehavior.Ignored)
        {
            _actionCall = actionCall;
            _securityContext = securityContext;
            _urls = urls;
            _request = request;
            _writer = writer;
        }

        protected override DoNext performInvoke()
        {
            if(SecurityProvider.HasPermissionForMethod(_actionCall.HandlerType, _actionCall.Method))
            {
                if (!_securityContext.IsAuthenticated())
                {
                    var model = _request.Get<ReturnUrlModel>();
                    var url = _urls.UrlFor(new LoginFormModel { ReturnUrl = model.RawUrl, Message = "You must be logged to view this page"});
                    _writer.RedirectToUrl(url);
                    return DoNext.Stop;
                }

                var validPermissions = SecurityProvider.GetPermissionsForMethod(_actionCall.HandlerType, _actionCall.Method);

                foreach (var perm in validPermissions)
                {
                    if (_securityContext.CurrentUser.IsInRole(perm.DisplayName))
                        return DoNext.Continue;
                }

                // Redirect to 403
                return DoNext.Continue;
                
            }
      

            return DoNext.Continue;
        }
    }

    public class ReturnUrlModel
    {
        public string RawUrl { get; set; }
    }
}