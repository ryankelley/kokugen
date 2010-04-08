using System;
using System.Collections.Generic;
using System.Linq;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.TimeRecord
{
    public class StopAction
    {
        private readonly ITimeRecordService _timeRecordService;

        public StopAction(ITimeRecordService timeRecordService)
        {
            _timeRecordService = timeRecordService;
        }

        public AjaxResponse Stop(StopTimeRecordModel model)
        {
            var timeRecord = _timeRecordService.GetTimeRecord(model.Id);
            timeRecord.Stop();
            timeRecord.ComputeDuration();
            _timeRecordService.Save(timeRecord);
            return new AjaxResponse{Success = true };
        }
    }

    public class StopTimeRecordFormModel
    {
        public Core.Domain.TimeRecord TimeRecord { get; set; }
    }
}