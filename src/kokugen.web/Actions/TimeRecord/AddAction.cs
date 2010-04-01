using Kokugen.Core;
using Kokugen.Core.Attributes;
using Kokugen.Core.Services;
using Kokugen.Core.Validation;

namespace Kokugen.Web.Actions.TimeRecord
{
    public class AddAction
    {
        private readonly IProjectService _projectService;

        public AddAction(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public AjaxResponse Command(AddTimeRecordModel inModel)
        {
            var timeRecord = new Core.Domain.TimeRecord()
                           {
                               Description = inModel.TimeRecordDescription,
                               Task = inModel.TimeRecordTask
                           };

            var project = _projectService.GetProjectFromId(inModel.TimeRecordProjectId);

            project.AddTime(timeRecord);
           
            var notification = _projectService.SaveProject(project);

            if (notification.IsValid())
                return new AjaxResponse()
                           {
                               Success = true,
                               Item = timeRecord
                           };

            return new AjaxResponse() {Success = false};
        }
    }

    public class TimeRecordFormModel
    {
        public Core.Domain.TimeRecord TimeRecord{ get; set; }
        
    }
}