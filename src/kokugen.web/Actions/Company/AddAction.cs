using FubuMVC.Core;
using Kokugen.Core.Domain;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.Company
{
    public class AddAction
    {
      private readonly ICompanyService _companyService;

        public AddAction(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public AjaxResponse Command(AddCompanyInput model)
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
        public Address Address { get; set; }
    }
}