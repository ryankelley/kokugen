using System;
using System.Web.Security;
using FubuMVC.Core;
using FubuMVC.Core.View;
using Kokugen.Core.Membership.Security;
using Kokugen.Core.Membership.Services;
using Kokugen.Web.Conventions;

namespace Kokugen.Web.Actions.Users.Details
{
    public class DetailsAction
    {
        private readonly IUserService _userService;

        public DetailsAction(IUserService userService)
        {
            _userService = userService;
        }

        public UserDetailsModel Query(UserDetailsRequest request)
        {
            return new UserDetailsModel(){User = _userService.GetUserByLogin(request.Username)};
        }
    }

    public class UserDetailsRequest
    {
        [RouteInput]
        public string Username { get; set; }
    }

    public class UserDetailsModel
    {
        public IUser User { get; set; }
    }

    public class Details : FubuPage<UserDetailsModel>{}
}