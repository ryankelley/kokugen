using System;
using AutoMapper;
using Kokugen.Core;
using Kokugen.Core.Attributes;
using Kokugen.Core.Services;
using Kokugen.Web.Actions.DTO;

namespace Kokugen.Web.Actions.TimeRecord
{
    public class AddAction
    {
        private readonly IProjectService _projectService;
        private readonly ITaskCategoryService _taskCategoryService;

        public AddAction(IProjectService projectService, ITaskCategoryService taskCategoryService)
        {
            _projectService = projectService;
            _taskCategoryService = taskCategoryService;
        }

        public AjaxResponse Command(AddTimeRecordModel inModel)
        {
            var task = _taskCategoryService.Get(inModel.TaskId);
            
            var project = _projectService.GetProjectFromId(inModel.ProjectId);
            
            var timeRecordDTO = new TimeRecordDTO()
                                    {
                                        Description = inModel.TimeRecordDescription,
                                        Task = task,
                                        
                                    };

            

            var timeRecord = new Core.Domain.TimeRecord();

            Mapper.DynamicMap(timeRecordDTO, timeRecord);

            timeRecord.Start();

            project.AddTime(timeRecord);
            
            var notification = _projectService.SaveProject(project);

            var timeRecord1 = new Core.Domain.TimeRecord();

            Mapper.DynamicMap(timeRecordDTO, timeRecord1);

            if (notification.IsValid())
                return new AjaxResponse()
                           {
                               Success = true,
                               Item = timeRecord1
                           };

            return new AjaxResponse() {Success = false};
        }
    }

    public class TimeRecordFormModel
    {
        public TimeRecordDTO TimeRecord{ get; set; }
        [ValueOf("Project")]
        public ValueObject ProjectId { get; set; }

        [ValueOf("TaskCategory")]
        public ValueObject TaskId { get; set; }
    }

    public class ProjectTimeRecordFormModel
    {
        public TimeRecordDTO TimeRecord { get; set; }
        
        public Guid ProjectId { get; set; }

        [ValueOf("TaskCategory")]
        public ValueObject TaskId { get; set; }
    }
}