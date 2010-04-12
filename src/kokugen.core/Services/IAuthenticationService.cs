using System;
using System.Security.Principal;
using System.Web.Security;
using FubuMVC.Core.Security;
using Kokugen.Core.Membership.Security;
using Kokugen.Core.Membership.Services;

namespace Kokugen.Core.Services
{
    public interface IAuthenticationService
    {
        void AfterUserAuthenticated();
        IUser GetCurrentLoggedInUser();
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

        public IUser GetCurrentLoggedInUser()
        {
            var name = _context.CurrentIdentity.Name;
            return name != string.Empty ? _userService.GetUserByLogin(name) : null;
        }
    }
}