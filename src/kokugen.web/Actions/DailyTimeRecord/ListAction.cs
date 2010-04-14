using System.Collections.Generic;
using System.Linq;
using Kokugen.Core.Services;
using Kokugen.Web.Actions.TimeRecord;

namespace Kokugen.Web.Actions.DailyTimeRecord
{
    public class ListAction
    {
        private readonly IDailyTimeRecordService _dailyTimeRecordService;


        public ListAction(IDailyTimeRecordService dailyTimeRecordService)
        {
            _dailyTimeRecordService = dailyTimeRecordService;
        }

        public DailyTimeRecordListModel Query(DailyTimeRecordListModel listModel)
        {
            return new DailyTimeRecordListModel()
                       {
                           DailyTimeRecords = _dailyTimeRecordService.GetAllTimeRecords().OrderByDescending(x => x.StartTime)
                       };
        }
    }

    public class DailyTimeRecordListModel
    {
        public IEnumerable<Core.Domain.DailyTimeRecord> DailyTimeRecords { get; set; }
    }
}