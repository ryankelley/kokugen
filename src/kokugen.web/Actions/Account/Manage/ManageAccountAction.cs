using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FubuMVC.Core;
using FubuMVC.Core.View;
using Kokugen.Core.Domain;
using Kokugen.Core.Membership.Services;
using Kokugen.Core.Validation;
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

        public AjaxResponse Command(ManageAccountModel model)
        {
            var user = _userService.Retrieve(model.Id);

            user.FirstName = model.User.FirstName;
            user.LastName = model.User.LastName;
            user.Email = model.User.Email;

            var validation = _userService.Update(user);

            if(validation.IsValid())
                return new AjaxResponse(){Success = true, Item = "Save successful!"};


            var messages = validation.AllMessages.Select(x=>x.Message).ToArray();

            return new AjaxResponse(){Item = messages};
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