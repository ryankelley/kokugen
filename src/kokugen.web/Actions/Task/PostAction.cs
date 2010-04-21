using System;
using AutoMapper;
using Kokugen.Core;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.Task
{
    public class TaskAction
    {
        private readonly ITaskService _taskService;
        private readonly ICardService _cardService;

        public TaskAction(ITaskService taskService, ICardService cardService)
        {
            _taskService = taskService;
            _cardService = cardService;
        }

        public AjaxResponse Command(TaskPostRequest model)
        {
            Core.Domain.Task task = null;
            if (model.Id.IsEmpty())
            {
                var card = _cardService.GetCard(model.CardId);

                task = new Core.Domain.Task
                           {
                               Description = model.Description
                           };

                card.AddTask(task);
                _cardService.SaveCard(card);
                _taskService.Update(task);
            }
            else
            {
                task = _taskService.Retreive(model.Id);

                task.Description = model.Description;

                _taskService.Update(task);
            }
            return new AjaxResponse {Success = true, Item = Mapper.DynamicMap<TaskDTO>(task)};
        }

        public AjaxResponse Remove(TaskRemoveRequest model)
        {
            var task = _taskService.Retreive(model.Id);
            _taskService.Destroy(task);
            return new AjaxResponse();
        }

    }

    public class TaskRemoveRequest
    {
        public Guid Id { get; set; }
    }

    public class TaskPostRequest
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        public Guid CardId { get; set; }
    }
}