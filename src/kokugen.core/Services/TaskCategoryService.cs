using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kokugen.Core.Domain;
using Kokugen.Core.Persistence.Repositories;

namespace Kokugen.Core.Services
{
    public interface ITaskCategoryService
    {
        IEnumerable<TaskCategory> GetAllCategories();
    }

    public class TaskCategoryService : ITaskCategoryService
    {
        private readonly ITaskCategoryRepository _taskCategoryRepository;

        public TaskCategoryService(ITaskCategoryRepository taskCategoryRepository)
        {
            _taskCategoryRepository = taskCategoryRepository;
        }

        public IEnumerable<TaskCategory> GetAllCategories()
        {
            return _taskCategoryRepository.Query();
        }
    }
}
