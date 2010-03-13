using System;
using System.Collections.Generic;
using Kokugen.Core.Services;
using Kokugen.Web.Conventions;

namespace Kokugen.Web.Actions.Project
{
    public class ListAction
    {
        private readonly IProjectService _projectService;
        private readonly IValueObjectInitializer _initLists;

        public ListAction(IProjectService projectService, IValueObjectInitializer initLists)
        {
            _projectService = projectService;
            _initLists = initLists;
        }

        public ProjectListModel Query( ProjectListModel projectListModel)
        {
            _initLists.Start();

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