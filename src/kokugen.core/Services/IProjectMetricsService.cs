using System;
using System.Collections.Generic;
using Kokugen.Core.Domain;

namespace Kokugen.Core.Services
{
    public interface IProjectMetricsService
    {
        ProjectMetricsDTO GetAverageMetrics(Project project);
    }

    public class ProjectMetricsService : IProjectMetricsService
    {
        private readonly ICardService _cardService;

        public ProjectMetricsService(ICardService cardService)
        {
            _cardService = cardService;
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