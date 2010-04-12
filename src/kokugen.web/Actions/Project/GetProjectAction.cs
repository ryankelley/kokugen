using System;
using System.Collections.Generic;
using System.Linq;
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
            var timeRecords = project.GetTimeRecords().OrderByDescending(x => x.StartTime).ToList();
            return new ProjectModel() {Project = project, TimeRecords = timeRecords, ProjectId = project.Id};
        }
    }

    public class GetProjectModel : IRequestById
    {
        public Guid Id { get; set; }
    }

    public class ProjectModel : ProjectBaseViewModel
    {
        public Core.Domain.Project Project { get; set; }

        public IList<Core.Domain.TimeRecord> TimeRecords { get; set; }
    }
}