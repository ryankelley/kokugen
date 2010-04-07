using System.Web.Security;
using FubuMVC.Core.View;
using Kokugen.Core.Membership.Services;
using PagedList;

namespace Kokugen.Web.Actions.Users
{
    public class ListAction
    {
        private readonly IUserService _userService;

        public ListAction(IUserService userService)
        {
            _userService = userService;
        }

        public UserListModel Query(UserListRequest request)
        {
            return new UserListModel() {Users = _userService.FindAll(request.Page, 10)};
        }

    }

    public class UserListRequest    
    {
        public int Page { get; set;}
    }

    public class UserListModel
    {
       public IPagedList<MembershipUser> Users { get; set; }
    }

    public class List : FubuPage<UserListModel>{}
}