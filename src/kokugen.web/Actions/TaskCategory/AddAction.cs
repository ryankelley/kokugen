using Kokugen.Core;
using Kokugen.Core.Attributes;
using Kokugen.Core.Services;
using Kokugen.Core.Validation;

namespace Kokugen.Web.Actions.TaskCategory
{
    public class AddAction
    {
        private readonly ITaskCategoryService _taskCategoryService;

        public AddAction(ITaskCategoryService taskCategoryService)
        {
            _taskCategoryService = taskCategoryService;
        }

        public AjaxResponse Command(AddTaskModel inModel)
        {
            var task = new Core.Domain.TaskCategory() {Name = inModel.Name};
          
           
            var notification = _taskCategoryService.SaveTask(task);

            if (notification.IsValid())
                return new AjaxResponse()
                           {
                               Success = true,
                               Item = task
                               //Item = new
                               //           {
                               //               Name = project.Name,
                               //               Description = project.Description,
                               //               Id = project.Id,
                               //               CompanyId = project.Company.Id,
                               //               CompanyName = project.Company.Name
                               //           }
                           }
                    ;
            return new AjaxResponse() {Success = false};
        }
    }

    public class AddTaskModel
    {
        public string Name { get; set; }
    }

}