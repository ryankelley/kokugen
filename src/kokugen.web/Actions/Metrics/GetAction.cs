using System;
using FubuMVC.Core.View;
using Kokugen.Core.Services;
using Kokugen.Web.Actions.Metrics;
using Kokugen.Web.Conventions;

namespace Kokugen.Web.Actions.Metrics
{
    public class GetAction
    {
        private readonly IProjectService _projectService;
        private readonly IProjectMetricsService _projectMetricsService;

        public GetAction(IProjectService projectService, IProjectMetricsService projectMetricsService)
        {
            _projectService = projectService;
            _projectMetricsService = projectMetricsService;
        }

        public MetricsModel Query(MetricsRequestModel model)
        {
            var project = _projectService.GetProjectFromId(model.Id);

            var metrics = _projectMetricsService.GetAverageMetrics(project);

            return new MetricsModel(){ Metrics = metrics, ProjectId = project.Id};
            
        }
    }

    public class MetricsRequestModel : IRequestById
    {
        public Guid Id { get; set; }
    }

    public class MetricsModel : ProjectBaseViewModel
    {
        public ProjectMetricsDTO Metrics { get; set; }
    }

    public class Get : FubuPage<MetricsModel> { }
}

