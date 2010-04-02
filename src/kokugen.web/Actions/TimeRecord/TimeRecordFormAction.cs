using System;
using FubuMVC.Core;
using Kokugen.Web.Actions.DTO;
using Kokugen.Web.Actions.TaskCategory;

namespace Kokugen.Web.Actions.TimeRecord
{
    public class TimeRecordFormAction
    {
        [FubuPartial]
        public TimeRecordFormModel Execute(TimeRecordFormModel model)
        {
            var timeRecord = new TimeRecordDTO();

            timeRecord = model.TimeRecord;
            
            return new TimeRecordFormModel { TimeRecord = timeRecord, ProjectId = model.ProjectId, TaskId = model.TaskId};
        }
    }
}