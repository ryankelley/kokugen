using Kokugen.Core;
using Kokugen.Core.Attributes;
using Kokugen.Core.Services;
using Kokugen.Core.Validation;
using Kokugen.Web.Conventions;

namespace Kokugen.Web.Actions.Project
{
    public class AddAction
    {
        private readonly IProjectService _projectService;
        private readonly ICompanyService _companyService;

        public AddAction(IProjectService projectService, ICompanyService companyService)
        {
            _projectService = projectService;
            _companyService = companyService;
        }

        public AjaxResponse Command(AddProjectModel inModel)
        {
            var company = _companyService.Get(inModel.CompanyId);

            var project = inModel.Project;

            project.Company = company;
            var notification = _projectService.SaveProject(project);



            if (notification.IsValid())
                return new AjaxResponse()
                           {
                               Success = true,
                               Item = new
                                          {
                                              Name = project.Name,
                                              Description = project.Description,
                                              Id = project.Id,
                                              CompanyId = project.Company.Id,
                                              CompanyName = project.Company.Name
                                          }
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