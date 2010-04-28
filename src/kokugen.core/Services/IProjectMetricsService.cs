using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Kokugen.Core.Domain;
using Kokugen.Core.Persistence.Repositories;

namespace Kokugen.Core.Services
{
    public interface IProjectMetricsService
    {
        ProjectMetricsDTO GetAverageMetrics(Project project);
        XmlDocument BuildCumulativeFlowData(Project project);
    }

    public class ProjectMetricsService : IProjectMetricsService
    {
        private readonly ICardService _cardService;
        private readonly ICardRepository _cardRepository;

        public ProjectMetricsService(ICardService cardService, ICardRepository cardRepository)
        {
            _cardService = cardService;
            _cardRepository = cardRepository;
        }

        public ProjectMetricsDTO GetAverageMetrics(Project project)
        {
            var cards = _cardService.GetCompleteCards(project);

            var totalLeadTime = new TimeSpan();
            var totalCycleTime = new TimeSpan();
            var totalWorkTime = new TimeSpan();
            var totalIdleTime = new TimeSpan();
            var totalNumber = 0;
            foreach (var card in cards)
            {
                if (card.DateCompleted != null)
                {
                    var time = card.DateCompleted.Value - card.Created;
                    totalLeadTime += time;
                }

                if (card.DateCompleted != null && card.Started != null) 
                    totalCycleTime += card.DateCompleted.Value - card.Started.Value;

                totalWorkTime += card.TotalWorkTime();
                totalIdleTime += card.TotalIdleTime();
                totalNumber++;
            }

            var leadTime = new TimeSpan(totalLeadTime.Ticks / totalNumber);
            var cycleTime = new TimeSpan(totalCycleTime.Ticks/totalNumber);
            var worktime = new TimeSpan(totalWorkTime.Ticks/totalNumber);
            var idleTime = new TimeSpan(totalIdleTime.Ticks/totalNumber);

            var efficiency = worktime.Seconds/cycleTime.Seconds;


            return new ProjectMetricsDTO
                       {
                           AverageLeadTime = leadTime,
                           AverageCycleTime = cycleTime,
                           AverageWorkTime = worktime,
                           AverageIdleTime = idleTime,
                           Efficiency = efficiency

                       };
        }

        public XmlDocument BuildCumulativeFlowData(Project project)
        {
            var flowData = _cardRepository.GetCumalitiveFlowForProject(project.Id);

            var cols = (from f in flowData
                        select f.ColumnId).Distinct().ToList();

            var dates = flowData.Select(x => x.Day).OrderBy(x => x.Date).Distinct().ToList();

            var output = new XmlDocument();

            var chartTag = output.CreateNode(XmlNodeType.Element, "chart", "");
            output.AppendChild(chartTag);

            var series = output.CreateNode(XmlNodeType.Element, "series", "");

            var totalDates = 0;
            foreach (var date in dates)
            {
                var data = output.CreateNode(XmlNodeType.Element, "value", "");
                var attr = output.CreateAttribute("xid");
                attr.Value = totalDates.ToString();
                data.Attributes.Append(attr);
                data.Value = date.ToShortDateString();

                series.AppendChild(data);
                totalDates++;
            }
            
            output.AppendChild(series);



            foreach (var colId in cols)
            {
                foreach (var data in flowData.Where(x => x.ColumnId == colId))
                {

                }
            }

            return output;
        }
    }

    public class ProjectMetricsDTO
    {
        public TimeSpan AverageCycleTime { get; set; }
        public TimeSpan AverageLeadTime { get; set; }

        public TimeSpan AverageWorkTime { get; set; }

        public TimeSpan AverageIdleTime { get; set; }

        public int Efficiency { get; set; }
    }
}