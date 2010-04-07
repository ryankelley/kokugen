using System;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.TimeRecord
{
    public class RemoveAction
    {
        
            private readonly IProjectService _projectService;

            public RemoveAction(IProjectService projectService)
            {
                _projectService = projectService;
            }

            public AjaxResponse Remove(RemoveTimeRecordInput model)
            {
                _projectService.DeleteTimeRecord(model.Id);
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