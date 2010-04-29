using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
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

            var noData = cards.Count() == 0;

            var leadTime = noData ? new TimeSpan(0) : new TimeSpan(totalLeadTime.Ticks / totalNumber);
            var cycleTime = noData ? new TimeSpan(0) : new TimeSpan(totalCycleTime.Ticks / totalNumber);
            var worktime = noData ? new TimeSpan(0) : new TimeSpan(totalWorkTime.Ticks / totalNumber);
            var idleTime = noData ? new TimeSpan(0) : new TimeSpan(totalIdleTime.Ticks / totalNumber);

            var efficiency = noData ? 0 : worktime.Seconds/cycleTime.Seconds;


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

            var projectCols = project.GetAllBoardColumns().ToList();
            projectCols.Reverse();

            var cols = projectCols.Select(x => x.Id).ToList();

            //TODO: What I really need to do is to make sure I print out values for all dates since the beginning of the project.
            // If I have dates with no cards, I should substitute 0

            //var cols = (from f in flowData
            //            join c in projectCols on f.ColumnId equals c.Id
            //            select f.ColumnId).Distinct().ToList();

            var dates = flowData.Select(x => x.Day).OrderBy(x => x.Date).Distinct().ToList();

            var totalDates = 0;

            var doc = new XDocument(
                new XDeclaration("1.0", Encoding.UTF8.HeaderName, String.Empty),
                new XElement("chart",
                             new XElement("series", BuildDateSeries(dates)),
                             new XElement("graphs", 
                                 BuildGraphSeries(cols, flowData))
                    )
                );
                    
                        
                        

            

            return doc.ToXmlDocument();
        }

        private XElement[] BuildGraphSeries(IEnumerable<Guid> cols, IEnumerable<CumalitiveFlowData> flowData)
        {
            var outputList = new List<XElement>();

            var graphid = 0;
            foreach (var colId in cols)
            {
                var graph = new XElement("graph",
                                         new XAttribute("gid", graphid),
                                         new XAttribute("title", flowData.Where(x => x.ColumnId == colId).First().ColumnName),
                                         new XAttribute("fill_alpha", 30),
                                         buildGraphData(flowData.Where(x => x.ColumnId == colId).ToList()));
                outputList.Add(graph);
                graphid++;
            }

            return outputList.ToArray();
        }

        private XElement[] buildGraphData(IEnumerable<CumalitiveFlowData> flowData)
        {
            var colData = new List<XElement>();
            var number = 0;
            foreach (var data in flowData)
            {
                colData.Add(new XElement("value",
                    new XAttribute("xid", number), data.NumberOfCards));
                number++;
            }
            return colData.ToArray();
        }

        private XElement[] BuildDateSeries(IEnumerable<DateTime> dates)
        {
            var list = new List<XElement>();
            var totalCount = 0;
            foreach (var date in dates)
            {
                list.Add(new XElement("value", 
                    new XAttribute("xid", totalCount), 
                    date.Month + "/" + date.Day));
                totalCount++;
            }

            return list.ToArray();
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