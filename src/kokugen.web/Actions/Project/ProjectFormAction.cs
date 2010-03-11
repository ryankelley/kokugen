using FubuMVC.Core;

namespace Kokugen.Web.Actions.Project
{
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
}