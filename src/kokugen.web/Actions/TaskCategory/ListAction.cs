using System.Linq;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.TaskCategory
{
    public class ListAction
    {
        private readonly ITaskCategoryService _taskCategoryService;

        public ListAction(ITaskCategoryService taskCategoryService)
        {
            _taskCategoryService = taskCategoryService;
        }

        public TaskCategoryListModel Query(TaskCategoryListModel model )
        {
            return new TaskCategoryListModel
                       {
                           Id = model.Id,
                           TaskCategories = _taskCategoryService.GetAllCategories().ToList()
                       };
        }

        
    }
}