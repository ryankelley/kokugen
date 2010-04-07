using System;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.TimeRecord
{
    public class RemoveAction
    {
        private readonly ITimeRecordService _timeRecordService;


        public RemoveAction(ITimeRecordService timeRecordService)
            {
                _timeRecordService = timeRecordService;
            }

        public AjaxResponse Remove(RemoveTimeRecordInput model)
            {
                _timeRecordService.DeleteTimeRecord(model.Id);
                return new AjaxResponse
                {
                    Success = true
                };
            }
        }

        public class RemoveTimeRecordInput
        {
            public Guid Id { get; set; }
        }
    
}