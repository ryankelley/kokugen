using System.Collections.Generic;
using System.Linq;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.TimeRecord
{
    public class ListAction
    {
        private readonly ITimeRecordService _timeRecordService;

        public ListAction(ITimeRecordService timeRecordService)
        {
            _timeRecordService = timeRecordService;
        }

        public TimeRecordListModel Query(TimeRecordListModel listModel)
        {
            var timeRecords = _timeRecordService.GetAllTimeRecords().ToList();
            return new TimeRecordListModel() { TimeRecords = timeRecords };
        }
    }

    public class TimeRecordListModel
    {
        public IList<Core.Domain.TimeRecord> TimeRecords { get; set; }
    }
}