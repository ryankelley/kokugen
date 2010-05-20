using System;
using System.Collections.Generic;
using Kokugen.Core.Domain;
using Kokugen.Core.Persistence.Repositories;
using Kokugen.Core.Validation;

namespace Kokugen.Core.Services
{
    public interface ITaskService
    {
        Task Retreive(Guid id);
        INotification Update(Task task);
        void Destroy(Task task);
        Task Complete(Guid id, bool isComplete, User user);
        bool ReOrderTasks(List<TaskOrderDTO> data);
    }
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IValidator _validator;

        public TaskService(ITaskRepository taskRepository, IValidator validator)
        {
            _taskRepository = taskRepository;
            _validator = validator;
        }

        public Task Retreive(Guid id)
        {
            return _taskRepository.Get(id);
        }

        public INotification Update(Task task)
        {
            var notification = _validator.Validate(task);

            if(notification.IsValid())
            {
                _taskRepository.Save(task);
            }
            return notification;
        }

        public void Destroy(Task task)
        {
            _taskRepository.Delete(task);
        }

        public Task Complete(Guid id, bool isComplete, User user)
        {
            var task = Retreive(id);
            if(isComplete)
            {
                task.IsComplete = true;
                task.CompletedDate = DateTime.Now;
                task.UserName = user.DisplayName();
            }
            else
            {
                task.IsComplete = false;
                task.CompletedDate = null;
                task.UserName = string.Empty;
            }

            Update(task);
            return task;
        }

        public bool ReOrderTasks(List<TaskOrderDTO> data)
        {
            data.Each(x =>
            {
                var task = _taskRepository.Get(x.Id);
                task.TaskOrder = x.TaskOrder;
                _taskRepository.Save(task);

            });

            return true;
        }
    }

    public class TaskOrderDTO
    {
        public Guid Id { get; set; }
        public int TaskOrder { get; set; }
    }
}