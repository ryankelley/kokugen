using System;
using AutoMapper;
using FubuMVC.Core;
using FubuMVC.Core.View;
using Kokugen.Core.Domain;
using Kokugen.Core.Membership.Services;
using Kokugen.Web.Actions.Account.Register;
using Kokugen.Web.Conventions;

namespace Kokugen.Web.Actions.Account.Manage
{
    public class ManageAccountAction
    {
        private readonly IUserService _userService;

        public ManageAccountAction(IUserService userService)
        {
            _userService = userService;
        }

        public ManageAccountModel Query(ManageAccountRequest request)
        {
            var user = _userService.GetUserByLogin(request.UserName);
            var dto = Mapper.DynamicMap<User, UserDTO>(user);
            return new ManageAccountModel(){User = dto, Id = user.Id};
        }

        public ManageAccountModel Command(ManageAccountModel model)
        {
            return model;
        }
    }

    public class ManageAccountRequest
    {
        [RouteInput]
        public string UserName { get; set; }
    }

    public class ManageAccount : FubuPage<ManageAccountModel>{}

    public class ManageAccountModel : IRequestById
    {
        public Guid Id { get; set; }
        public UserDTO User { get; set; }
    }
}