using System;
using System.Web.Security;
using FubuMVC.Core;
using FubuMVC.Core.View;
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
            return new UserDetailsModel(){User = _userService.Retrieve((Guid) request.Id)};
        }
    }

    public class UserDetailsRequest
    {
        [RouteInput]
        public Guid? Id { get; set;}
    }

    public class UserDetailsModel
    {
        public MembershipUser User { get; set; }
    }

    public class Details : FubuPage<UserDetailsModel>{}
}