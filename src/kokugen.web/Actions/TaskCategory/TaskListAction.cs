using System.Linq;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.TaskCategory
{
    public class TaskListAction
    {
        private readonly ITaskCategoryService _taskCategoryService;

        public TaskListAction(ITaskCategoryService taskCategoryService)
        {
            _taskCategoryService = taskCategoryService;
        }

        public TaskListModel Query(TaskListModel model )
        {
            return new TaskListModel
                       {
                           Id = model.Id,
                           TaskCategories = _taskCategoryService.GetAllCategories().ToList()
                       };
        }

        
    }
}