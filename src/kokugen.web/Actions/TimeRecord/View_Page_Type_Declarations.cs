using FubuMVC.Core.View;

namespace Kokugen.Web.Actions.TimeRecord
{
    public class TimeRecord_Control : FubuControl<Core.Domain.TimeRecord>{}
    public class TimeRecordForm : FubuPage<TimeRecordFormModel> { }
    public class StopTimeRecordForm : FubuPage<StopTimeRecordFormModel> {}
    public class ProjectTimeRecordForm : FubuPage<ProjectTimeRecordFormModel>{}
    public class List : FubuPage<TimeRecordListModel> { }
}