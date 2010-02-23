using System;
using FubuMVC.Core;
using Kokugen.Core.Persistence.Repositories;
using Kokugen.Core.Services;
using Kokugen.WebBackend.ViewModels;

namespace Kokugen.WebBackend.Handlers.Company
{
    public class AddHandler
    {
        private readonly ICompanyService _companyService;

        public AddHandler(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public AjaxResponse Execute(AddCompanyInput model)
        {
            if(model.CompanyName.IsEmpty()) return new AjaxResponse{ Success = false};

            var company = _companyService.AddCompany(model.CompanyName);

            return new AjaxResponse
                       {
                           Success = true,
                           Item = company
                       };
        }
    }

    public class AddCompanyInput
    {
        public string CompanyName { get; set; }
    }
}