using System;
using System.Linq;
using AutoMapper;
using FubuMVC.Core.Security;
using Kokugen.Core;
using Kokugen.Core.Attributes;
using Kokugen.Core.Membership.Services;
using Kokugen.Core.Services;
using Kokugen.Web.Actions.DTO;

namespace Kokugen.Web.Actions.TimeRecord
{
    public class AddAction
    {
        private readonly ITimeRecordService _timeRecordService;
        private readonly IProjectService _projectService;
        private readonly ITaskCategoryService _taskCategoryService;
        private readonly IUserService _userService;
        private readonly ISecurityContext _securityContext;

        public AddAction(ITimeRecordService timeRecordService, IProjectService projectService, ITaskCategoryService taskCategoryService, IUserService userService, ISecurityContext securityContext)
        {
            _timeRecordService = timeRecordService;
            _projectService = projectService;
            _taskCategoryService = taskCategoryService;
            _userService = userService;
            _securityContext = securityContext;
        }

        public AjaxResponse Command(AddTimeRecordModel inModel)
        {
            var task = _taskCategoryService.Get(inModel.TaskId);
            
            var project = _projectService.GetProjectFromId(inModel.ProjectId);
            
            var user = inModel.UserId.IsEmpty() ? _userService.GetUserByLogin(_securityContext.CurrentIdentity.Name) : _userService.GetUserById(inModel.UserId);

            var timeRecord = new Core.Domain.TimeRecord();

            timeRecord.Project = project;
            timeRecord.Task = task;
            timeRecord.User = user;

            if(inModel.CardId.IsNotEmpty())
            {
                var card = project.GetCards().Where(x => x.Id == inModel.CardId).FirstOrDefault();
                timeRecord.Card = card;
            }

            timeRecord.Description = inModel.TimeRecordDescription;
            timeRecord.Start();
            project.AddTime(timeRecord);
            
            var notification = _projectService.SaveProject(project);
            _timeRecordService.Save(timeRecord);
            var output = Mapper.DynamicMap<Core.Domain.TimeRecord, TimeRecordDTO>(timeRecord);

            if (notification.IsValid())
                return new AjaxResponse()
                           {
                               Success = true,
                               Item = output
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