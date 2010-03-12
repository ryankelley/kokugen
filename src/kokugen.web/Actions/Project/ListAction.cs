using System;
using System.Collections.Generic;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.Project
{
    public class ListAction
    {
        private readonly IProjectService _projectService;

        public ListAction(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public ProjectListModel Query()
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