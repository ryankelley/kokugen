using System;
using AutoMapper;
using FubuMVC.Core.Security;
using Kokugen.Core;

using Kokugen.Core.Domain;
using Kokugen.Core.Membership.Services;
using Kokugen.Core.Services;


namespace Kokugen.Web.Actions.DailyTimeRecord
{
    public class AddAction
    {
        private readonly IDailyTimeRecordService _timeRecordService;
        private readonly IUserService _userService;
        private readonly ISecurityContext _securityContext;

        public AddAction(IDailyTimeRecordService timeRecordService,  IUserService userService, ISecurityContext securityContext)
        {
            _timeRecordService = timeRecordService;
          
            _userService = userService;
            _securityContext = securityContext;
        }

        public AjaxResponse Command(AddDailyTimeRecordModel inModel)
        {
            
            var user = inModel.UserId.IsEmpty() ? _userService.GetUserByLogin(_securityContext.CurrentIdentity.Name) : _userService.GetUserById(inModel.UserId);

            var timeRecord = new Core.Domain.DailyTimeRecord();

            timeRecord.User = user;
            
            timeRecord.Start();
            
            var notification = _timeRecordService.Save(timeRecord);
            var output = Mapper.DynamicMap<Core.Domain.DailyTimeRecord, DailyTimeRecordDTO>(timeRecord);

            if (notification.IsValid())
                return new AjaxResponse()
                           {
                               Success = true,
                               Item = output
                           };

            return new AjaxResponse() {Success = false};
        }
    }

    public class AddDailyTimeRecordModel
    {

        public DateTime StartTime;
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }

    public class DailyTimeRecordDTO
    {
        public double Duration { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public User User { get; set; }
        public Guid Id { get; set; }

    }

    public class DailyTimeRecordFormModel
    {
        public DailyTimeRecordDTO TimeRecord{ get; set; }
    }

   
}