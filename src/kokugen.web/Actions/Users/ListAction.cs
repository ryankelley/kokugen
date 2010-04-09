using System.Web.Security;
using FubuMVC.Core;
using FubuMVC.Core.Urls;
using FubuMVC.Core.View;
using Kokugen.Core.Membership.Security;
using Kokugen.Core.Membership.Services;
using PagedList;

namespace Kokugen.Web.Actions.Users
{
    public class ListAction
    {
        private readonly IUserService _userService;
        private readonly IUrlRegistry _urlRegistry;

        public ListAction(IUserService userService, IUrlRegistry urlRegistry)
        {
            _userService = userService;
            _urlRegistry = urlRegistry;
        }

        public UserListModel Query(UserListRequest request)
        {
            var users = _userService.FindAll(request.Page, 10);
            return new UserListModel()
                       {
                           Users = users,
                           FirstPageLink = _urlRegistry
                               .UrlFor(new UserListRequest() {Page = 0}),
                           LastPageLink = _urlRegistry
                               .UrlFor(new UserListRequest() {Page = users.PageCount - 1}),
                           NextLink = _urlRegistry
                               .UrlFor(new UserListRequest() {Page = users.PageIndex + 1}),
                           PrevLink = _urlRegistry
                               .UrlFor(new UserListRequest() {Page = users.PageIndex - 1})

                       };
        }
    }

    public class UserListRequest    
    {
        [RouteInput]
        public int Page { get; set;}
    }

    public class UserListModel
    {
       public IPagedList<IUser> Users { get; set; }
       public string LastPageLink { get; set; }
       public string FirstPageLink { get; set; }
       public string NextLink { get; set; }
       public string PrevLink { get; set; }
    }

    public class List : FubuPage<UserListModel>{}
}