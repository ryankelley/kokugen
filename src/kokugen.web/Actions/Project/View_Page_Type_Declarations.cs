using FubuMVC.Core.View;

namespace Kokugen.Web.Actions.Project
{
    public class List : FubuPage<ProjectListModel> { }
    public class GetProject : FubuPage<ProjectModel> { }
    public class ProjectItem_Control : FubuControl<Kokugen.Core.Domain.Project> { }
    public class ProjectForm : FubuPage<ProjectFormModel> {}
}