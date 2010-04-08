using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Kokugen.Core;
using Kokugen.Core.Services;
using Kokugen.Web.Actions.DTO;

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
            if (model.Id.IsNotEmpty())
            {
                var timeRecord = _timeRecordService.GetTimeRecord(model.Id);

                if (model.Billable != 0)
                {
                    timeRecord.Billable = model.Billable;

                }
                else
                {
                    timeRecord.Stop();
                    timeRecord.ComputeDuration();
                }
                _timeRecordService.Save(timeRecord);
                return new AjaxResponse {Success = true, Item = Mapper.DynamicMap<TimeRecordDTO>(timeRecord)};
            }
            return new AjaxResponse() {Success = false};    
        }
    }

    public class StopTimeRecordFormModel
    {
        public Core.Domain.TimeRecord TimeRecord { get; set; }
    }
}