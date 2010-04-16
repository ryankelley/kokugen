using System.Linq;
using Kokugen.Core.Domain;
using Kokugen.Core.Membership.Services;
using Kokugen.Core.Services;

namespace Kokugen.Core
{
    public interface IValueObjectInitializer : IStartable
    {
    }

    public class ValueObjectInitializer : IValueObjectInitializer
    {
        private readonly ICompanyService _companyService;
        private readonly ITaskCategoryService _taskCategoryService;
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;

        public ValueObjectInitializer(ICompanyService companyService, ITaskCategoryService taskCategoryService, IProjectService projectService, IUserService userService)
        {
            _companyService = companyService;
            _taskCategoryService = taskCategoryService;
            _projectService = projectService;
            _userService = userService;
        }

        public void Start()
        {
            var list = _companyService.ListAllCompanies().Select(x => new ValueObject(x.Id.ToString(), x.Name));
            var taskList = _taskCategoryService.GetAllCategories().Select(x => new ValueObject(x.Id.ToString(), x.Name));
            var projectList = _projectService.ListProjects().Select(x => new ValueObject(x.Id.ToString(), x.Name));
            var userList = _userService.FindAll().Where(x => x.IsActivated).Select(x => new ValueObject(x.Id.ToString(), x.DisplayName()));
            ValueObjectRegistry.AddValueObjects<Company>(list);
            ValueObjectRegistry.AddValueObjects<TaskCategory>(taskList);
            ValueObjectRegistry.AddValueObjects<Project>(projectList);
            ValueObjectRegistry.AddValueObjects<User>(userList);
        }
    }
}