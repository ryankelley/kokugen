using FubuMVC.Core;
using Kokugen.Core.Attributes;
using Kokugen.Core.Services;
using Kokugen.Core.Validation;
using Kokugen.Web.Conventions;

namespace Kokugen.Web.Actions.Project
{
    public class AddAction
    {
        private readonly IProjectService _projectService;

        public AddAction(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public AjaxResponse Command(AddProjectModel inModel)
        {
            var notification = _projectService.SaveProject(inModel.Project);

            if (notification.IsValid())
                return new AjaxResponse() {Success = true, Item = inModel.Project};
            return new AjaxResponse() {Success = false};
        }
    }

    public class ProjectFormAction
    {
        [FubuPartial]
        public ProjectFormModel Execute(ProjectFormModel model)
        {
            var project = new Core.Domain.Project();

            project = model.Project;

            return new ProjectFormModel {Project = project};
        }
    }

    public class ProjectFormModel
    {
        public Core.Domain.Project Project { get; set; }

        [ValueOf("Company"), Required]
        public ValueObject CompanyId { get; set; }
    }
}