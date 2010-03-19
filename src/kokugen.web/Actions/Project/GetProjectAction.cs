using System;
using Kokugen.Core.Services;
using Kokugen.Web.Conventions;

namespace Kokugen.Web.Actions.Project
{
    public class GetProjectAction
    {
        private readonly IProjectService _projectService;

        public GetProjectAction(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public ProjectModel Query(GetProjectModel model)
        {
            var project = _projectService.GetProjectFromId(model.Id);
            return new ProjectModel() {Project = project };
        }
    }

    public class GetProjectModel : IRequestById
    {
        public Guid Id { get; set; }
    }

    public class ProjectModel
    {
        public Core.Domain.Project Project { get; set; }
    }
}