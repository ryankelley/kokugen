using System;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.TaskCategory
{
    public class RemoveAction
    {

        private readonly ITaskCategoryService _taskService;

        public RemoveAction(ITaskCategoryService taskService)
        {
            _taskService = taskService;
        }

        public AjaxResponse Remove(RemoveTaskInput model)
        {
            _taskService.DeleteTask(model.Id);
            return new AjaxResponse
                       {
                           Success = true
                       };
        }
    }

    public class RemoveTaskInput
    {
        public Guid Id { get; set; }
    }
}