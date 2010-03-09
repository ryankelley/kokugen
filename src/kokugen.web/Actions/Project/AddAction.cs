using FubuMVC.Core;

namespace Kokugen.Web.Actions.Project
{
    public class AddAction
    {
        public AjaxResponse Command(AddProjectModel inModel)
        {
            
            return new AjaxResponse();
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
    }
}