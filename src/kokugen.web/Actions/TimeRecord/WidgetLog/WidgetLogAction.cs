using System;
using System.Linq;
using AutoMapper;
using Kokugen.Core;
using Kokugen.Core.Membership.Services;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.TimeRecord.WidgetLog
{
    public class WidgetLogAction
    {
        private readonly ITimeRecordService _timeRecordService;
        private readonly IUserService _userService;

        public WidgetLogAction(ITimeRecordService timeRecordService, IUserService userService)
        {
            _timeRecordService = timeRecordService;
            _userService = userService;
        }

        public AjaxResponse Command(WidgetLogRequest model)
        {
            if(model.UserId.IsNotEmpty())
            {
                var user = _userService.GetUserById(model.UserId);
                var records = _timeRecordService.FindAll(user, 10).Select(x => Mapper.DynamicMap<TimeLogItem>(x)).ToList();

                return new AjaxResponse { Success = true, Item = records};
            }

            return new AjaxResponse {Success = false};

        }
    }

    public class WidgetLogRequest
    {
        public Guid UserId { get; set; }
    }

    public class TimeLogItem
    {
        public string Description { get; set;}
        public string ProjectName { get; set;}
        public string CardTitle { get; set; }
        public double Duration { get; set; }
        public double Billable { get; set; }
    }
}