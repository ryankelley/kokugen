using FubuMVC.Core;
using Kokugen.Web.Actions.TaskCategory;

namespace Kokugen.Web.Actions.TimeRecord
{
    public class TimeRecordFormAction
    {
        [FubuPartial]
        public TimeRecordFormModel Execute(TimeRecordFormModel model)
        {
            var timeRecord = new Core.Domain.TimeRecord();

            
            return new TimeRecordFormModel { TimeRecord = timeRecord };
        }
    }
}