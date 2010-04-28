using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Kokugen.Core.Services;
using Kokugen.Web.Actions.DTO;

namespace Kokugen.Web.Actions.TimeRecord
{
    public class TimeRecordListAction
    {
        private readonly ITimeRecordService _timeRecordService;

        public TimeRecordListAction(ITimeRecordService timeRecordService)
        {
            _timeRecordService = timeRecordService;
        }

        public TimeRecordListModel Query(TimeRecordListModel listModel)
        {
            return new TimeRecordListModel()
                       {
                           TimeRecords = _timeRecordService.GetAllTimeRecords().OrderByDescending(x => x.StartTime)
                       };
        }
    }

    public class TimeRecordListModel
    {
        public IEnumerable<Core.Domain.TimeRecord> TimeRecords { get; set; }
    }
}