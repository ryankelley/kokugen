using System;
using System.Collections.Generic;

namespace Kokugen.Web.Actions.TaskCategory
{
    public class TaskCategoryListModel
    {
        public Guid Id { get; set; }
        public IEnumerable<Core.Domain.TaskCategory> TaskCategories { get; set; }
    }
}