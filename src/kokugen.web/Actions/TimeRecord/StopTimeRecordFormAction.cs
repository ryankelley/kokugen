using System;
using FubuMVC.Core;
using Kokugen.Web.Actions.DTO;
using Kokugen.Web.Actions.TaskCategory;

namespace Kokugen.Web.Actions.TimeRecord
{
    public class StopTimeRecordFormAction
    {
        [FubuPartial]
        public StopTimeRecordFormModel Execute(StopTimeRecordFormModel model)
        {
            var timeRecord = new Core.Domain.TimeRecord();

            timeRecord = model.TimeRecord;
            
            return new StopTimeRecordFormModel { TimeRecord = timeRecord};
        }
    }
}