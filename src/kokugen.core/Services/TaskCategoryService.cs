using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kokugen.Core.Domain;
using Kokugen.Core.Persistence.Repositories;
using Kokugen.Core.Validation;

namespace Kokugen.Core.Services
{
    public interface ITaskCategoryService
    {
        IEnumerable<TaskCategory> GetAllCategories();
        INotification SaveTask(TaskCategory taskCategory);
        TaskCategory Get(Guid taskId);
        void DeleteTask(Guid guid);
    }

    public class TaskCategoryService : ITaskCategoryService
    {
        private readonly ITaskCategoryRepository _taskCategoryRepository;
        private readonly IValidator _validator;

        public TaskCategoryService(ITaskCategoryRepository taskCategoryRepository, IValidator validator)
        {
            _taskCategoryRepository = taskCategoryRepository;
            _validator = validator;
        }

        public IEnumerable<TaskCategory> GetAllCategories()
        {
            return _taskCategoryRepository.Query();
        }

        public INotification SaveTask(TaskCategory taskCategory)
        {
            var notification = _validator.Validate(taskCategory);

            if(notification.IsValid())
            {
                _taskCategoryRepository.Save(taskCategory);
            }

            return notification;
        }

        public TaskCategory Get(Guid taskId)
        {
            return _taskCategoryRepository.Get(taskId);
        }

        public void DeleteTask(Guid guid)
        {
            var task=_taskCategoryRepository.Get(guid);
            _taskCategoryRepository.Delete(task);
        }
    }
}
