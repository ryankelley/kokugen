using System;
using System.Xml;
using FubuMVC.Core;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.Metrics.CumalativeFlow
{
    public class GetData
    {
        private readonly IProjectMetricsService _projectMetricsService;
        private readonly IProjectService _projectService;

        public GetData(IProjectMetricsService projectMetricsService, IProjectService projectService)
        {
            _projectMetricsService = projectMetricsService;
            _projectService = projectService;
        }

        public XmlDocument Query(FlowDataRequest model)
        {
            var project = _projectService.GetProjectFromId(model.ProjectId);

            return _projectMetricsService.BuildCumulativeFlowData(project);
        }
    }

    public class FlowDataRequest
    {
        [QueryString]
        public Guid ProjectId { get; set; }
    }
}