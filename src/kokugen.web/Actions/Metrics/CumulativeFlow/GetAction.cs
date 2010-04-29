using System;
using System.Xml;
using FubuMVC.Core;
using Kokugen.Core.Services;
using Kokugen.Web.Conventions;

namespace Kokugen.Web.Actions.Metrics.CumalativeFlow
{
    public class GetAction
    {
        private readonly IProjectMetricsService _projectMetricsService;
        private readonly IProjectService _projectService;

        public GetAction(IProjectMetricsService projectMetricsService, IProjectService projectService)
        {
            _projectMetricsService = projectMetricsService;
            _projectService = projectService;
        }

        public XmlResponse Query(FlowDataRequest model)
        {
            var project = _projectService.GetProjectFromId(model.ProjectId);

            return new XmlResponse {XmlData = _projectMetricsService.BuildCumulativeFlowData(project)};
        }
    }

    public class FlowDataRequest
    {
        [QueryString]
        public Guid ProjectId { get; set; }

    }
}