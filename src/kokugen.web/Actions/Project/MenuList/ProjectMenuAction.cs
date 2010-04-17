using System;
using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core;
using FubuMVC.Core.View;
using Kokugen.Core.Domain;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.Project.MenuList
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

        public AjaxResponse Query(ProjectMenuModelJSON model)
        {
            var projects = _projectService.ListProjects().Where(x => x.Status == ProjectStatus.Active).Select(x => new ProjectMenuItem { Name = x.Name, Id = x.Id });

            return new AjaxResponse { Success = true, Item = projects };
        }
    }

    public class ProjectMenuModelJSON
    {
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

    public class ProjectMenu_Item : FubuControl<ProjectMenuItem> { }
    public class ProjectMenu : FubuPage<ProjectMenuModel> { }
}