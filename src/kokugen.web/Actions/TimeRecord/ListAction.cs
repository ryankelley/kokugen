using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Kokugen.Core.Services;
using Kokugen.Web.Actions.DTO;

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
            var timeRecords = _timeRecordService.GetAllTimeRecords().ToArray();
            var dtos = Mapper.Map<Kokugen.Core.Domain.TimeRecord[], TimeRecordDTO[]>(timeRecords);
            return new TimeRecordListModel() { TimeRecords = dtos };
        }
    }

    public class TimeRecordListModel
    {
        public IList<TimeRecordDTO> TimeRecords { get; set; }
    }
}