using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.Task.ReOrder
{
    public class TaskReorderAction
    {
        private readonly ITaskService _taskService;

        public TaskReorderAction(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public AjaxResponse Command(ReOrderTasksRequest model)
        {
            var data = new JavaScriptSerializer().Deserialize<List<TaskOrderDTO>>(model.Tasks);

            _taskService.ReOrderTasks(data);


            return new AjaxResponse();
        }
    }

    

    public class ReOrderTasksRequest
    {
        public Guid CardId { get; set; }
        public string Tasks { get; set; }
    }
}