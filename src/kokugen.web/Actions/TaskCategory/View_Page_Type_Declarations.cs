using FubuMVC.Core.View;
using Kokugen.Web.Actions.Project;
using Kokugen.Web.Actions.TaskCategory;

namespace Kokugen.Web.Actions.TaskCategory
{
    public class List : FubuPage<TaskCategoryListModel> { }
    public class TaskItem_Control : FubuControl<Core.Domain.TaskCategory> { }
    public class TaskForm : FubuPage<AddTaskModel> { }

    
}