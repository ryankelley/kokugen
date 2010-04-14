using System;
using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.Project
{
    public class ProjectMenuAction
    {
        private readonly IProjectService _projectService;

        public ProjectMenuAction(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [FubuPartial]
        public ProjectMenuModel Execute(ProjectMenuModel model)
        {
            var projects = _projectService.ListProjects().Select(x => new ProjectMenuItem {Name = x.Name, Id = x.Id});

            return new ProjectMenuModel {ProjectList = projects};
        }
    }

    public class ProjectMenuItem
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
    }

    public class ProjectMenuModel
    {
        public IEnumerable<ProjectMenuItem> ProjectList { get; set; }
    }
}