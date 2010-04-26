using System;
using AutoMapper;
using FubuCore;
using Kokugen.Core;
using Kokugen.Core.Services;
using Kokugen.Web.Actions.DTO;
using Kokugen.Web.Actions.TimeRecord;

namespace Kokugen.Web.Actions.DailyTimeRecord
{
    public class StopAction
    {
        private readonly IDailyTimeRecordService _dailyTimeRecordService;

        public StopAction(IDailyTimeRecordService dailyTimeRecordService)
        {
            _dailyTimeRecordService = dailyTimeRecordService;
        }

        public AjaxResponse Stop(StopDailyTimeRecordModel model)
        {
            if (model.Id.IsNotEmpty())
            {
                var timeRecord = _dailyTimeRecordService.Get(model.Id);

                timeRecord.Stop();
                timeRecord.ComputeDuration();
                
                _dailyTimeRecordService.Save(timeRecord);
                return new AjaxResponse {Success = true, Item = Mapper.DynamicMap<DailyTimeRecordDTO>(timeRecord)};
            }
            return new AjaxResponse() {Success = false};    
        }
    }

    public class StopDailyTimeRecordModel
    {
        public Guid Id { get; set; }

        public double Duration { get; set; }
        
    }
}