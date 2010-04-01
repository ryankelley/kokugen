using FubuMVC.Core;

namespace Kokugen.Web.Actions.TaskCategory
{
    public class TaskFormAction
    {
            [FubuPartial]
            public TaskFormModel Execute(TaskFormModel model)
            {
                var task = new Core.Domain.TaskCategory();

                task = model.Task;

                return new TaskFormModel { Task = task };
            }
    }
}