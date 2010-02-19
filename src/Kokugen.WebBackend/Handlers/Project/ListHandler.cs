using System;
using System.Collections.Generic;
using Kokugen.Core.Services;

namespace Kokugen.WebBackend.Handlers.Project
{
    public class ListHandler
    {
        private readonly IProjectService _projectService;

        public ListHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public ProjectListModel Execute()
        {
            return new ProjectListModel
                       {
                           Projects = _projectService.ListProjects()
                       };
        }
    }

    public class ProjectListModel
    {
        public IEnumerable<Core.Domain.Project> Projects { get; set; }
    }
}