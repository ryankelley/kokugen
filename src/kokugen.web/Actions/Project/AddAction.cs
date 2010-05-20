using FubuMVC.Core.Security;
using Kokugen.Core;
using Kokugen.Core.Attributes;
using Kokugen.Core.Membership.Services;
using Kokugen.Core.Services;
using Kokugen.Core.Validation;

namespace Kokugen.Web.Actions.Project
{
    public class AddAction
    {
        private readonly IProjectService _projectService;
        private readonly ICompanyService _companyService;
        private readonly IUserService _userService;
        private readonly ISecurityContext _securityContext;

        public AddAction(IProjectService projectService, 
            ICompanyService companyService,
            IUserService userService,
            ISecurityContext securityContext)
        {
            _projectService = projectService;
            _companyService = companyService;
            _userService = userService;
            _securityContext = securityContext;
        }

        public AjaxResponse Command(AddProjectModel inModel)
        {
            var company = _companyService.Get(inModel.CompanyId);

            var project = _projectService.CreateProject(inModel.ProjectName, inModel.ProjectDescription, company, _userService.GetUserByLogin(_securityContext.CurrentIdentity.Name));
            var notification = _projectService.SaveProject(project);

            if (notification.IsValid())
                return new AjaxResponse()
                           {
                               Success = true,
                               Item = project
                               //Item = new
                               //           {
                               //               Name = project.Name,
                               //               Description = project.Description,
                               //               ProjectId = project.ProjectId,
                               //               CompanyId = project.Company.ProjectId,
                               //               CompanyName = project.Company.Name
                               //           }
                           }
                    ;
            return new AjaxResponse() {Success = false};
        }
    }

    public class ProjectFormModel
    {
        public Core.Domain.Project Project { get; set; }

        [ValueOf("Company"), Required]
        public ValueObject CompanyId { get; set; }
    }
}