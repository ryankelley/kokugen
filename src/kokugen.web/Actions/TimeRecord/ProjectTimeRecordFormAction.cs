using System;
using FubuMVC.Core;
using Kokugen.Web.Actions.DTO;
using Kokugen.Web.Actions.TaskCategory;

namespace Kokugen.Web.Actions.TimeRecord
{
    public class ProjectTimeRecordFormAction
    {
        [FubuPartial]
        public ProjectTimeRecordFormModel Execute(ProjectTimeRecordFormModel model)
        {
            var timeRecord = new TimeRecordDTO();

            timeRecord = model.TimeRecord;

            return new ProjectTimeRecordFormModel { TimeRecord = timeRecord, ProjectId = model.ProjectId, TaskId = model.TaskId };
        }
    }
}