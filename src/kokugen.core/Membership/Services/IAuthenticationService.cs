using System;
using System.Security.Principal;
using System.Web.Security;
using FubuMVC.Core.Security;
using Kokugen.Core.Membership.Security;

namespace Kokugen.Core.Membership.Services
{
    public interface IAuthenticationService
    {
        void AfterUserAuthenticated();
        MembershipUser GetCurrentLoggedInUser();
    }

    public class AuthenticationService : IAuthenticationService
    {
        private ISecurityContext _context;
        private readonly IPrincipalFactory _factory;
        private readonly IUserService _userService;

        public AuthenticationService(ISecurityContext context, IPrincipalFactory factory, IUserService userService)
        {
            _context = context;
            _factory = factory;
            _userService = userService;
        }

        public void AfterUserAuthenticated()
        {
            if (!_context.IsAuthenticated()) return;

            IIdentity identity = _context.CurrentIdentity;
            _context.CurrentUser = _factory.CreatePrincipal(identity);
        }

        public MembershipUser GetCurrentLoggedInUser()
        {
            var userId = ((FubuPrincipal)_context.CurrentUser).UserId;
            return userId != Guid.Empty ? _userService.Retrieve(userId) : null;
        }
    }
}