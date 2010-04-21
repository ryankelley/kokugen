using System;
using AutoMapper;
using FubuMVC.Core.Security;
using Kokugen.Core;
using Kokugen.Core.Membership.Services;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.Task.Complete
{
    public class CompleteAction
    {
        private readonly ITaskService _taskService;
        private readonly IUserService _userService;
        private readonly ISecurityContext _securityContext;

        public CompleteAction(ITaskService taskService, IUserService userService, ISecurityContext securityContext)
        {
            _taskService = taskService;
            _userService = userService;
            _securityContext = securityContext;
        }

        public AjaxResponse Command(TaskCompleteRequest model)
        {
            if (model.Id.IsNotEmpty())
            {
                var user = _userService.GetUserByLogin(_securityContext.CurrentUser.Identity.Name);
                var task =_taskService.Complete(model.Id, model.IsComplete, user);

                return new AjaxResponse {Success = true, Item= Mapper.DynamicMap<TaskDTO>(task)};
            }
            return new AjaxResponse {Success = false};
        }
    }

    public class TaskCompleteRequest
    {
        public Guid Id { get; set; }
        public bool IsComplete { get; set; }
    }
}