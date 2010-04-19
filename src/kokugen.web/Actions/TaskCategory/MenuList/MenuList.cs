using System;
using System.Linq;
using FubuMVC.Core.View;
using Kokugen.Core.Services;
using Kokugen.Web.Conventions;

namespace Kokugen.Web.Actions.TaskCategory.MenuList
{
    public class TaskMenuListAction
    {
        private readonly ITaskCategoryService _taskCategoryService;

        public TaskMenuListAction(ITaskCategoryService taskCategoryService)
        {
            _taskCategoryService = taskCategoryService;
        }

        public AjaxResponse Query(TaskMenuListRequest request)
        {
            var list = _taskCategoryService.GetAllCategories().OrderBy(x => x.Name).Select(x => new {Id = x.Id, Name = x.Name});
            return new AjaxResponse {Success = true, Item = list};
        }

        
    }

    public class TaskMenuListRequest {
    }

   
}