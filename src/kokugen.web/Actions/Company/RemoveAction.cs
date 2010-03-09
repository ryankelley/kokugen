using System;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.Company
{
    public class RemoveAction
    {
        private readonly ICompanyService _companyService;

        public RemoveAction(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public AjaxResponse Command(RemoveCompanyInput model)
        {
            _companyService.DeleteCompany(model.Id);
            return new AjaxResponse
                       {
                           Success = true
                       };
        }
    }

    public class RemoveCompanyInput
    {
        public Guid Id { get; set; }
    }
}