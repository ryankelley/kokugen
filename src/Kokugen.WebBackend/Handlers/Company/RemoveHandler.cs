using System;
using Kokugen.Core.Services;
using Kokugen.WebBackend.ViewModels;

namespace Kokugen.WebBackend.Handlers.Company
{
    public class RemoveHandler
    {
        private readonly ICompanyService _companyService;

        public RemoveHandler(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public AjaxResponse Execute(RemoveCompanyInput model)
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