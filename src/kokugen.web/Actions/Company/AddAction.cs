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

            var company = _companyService.AddCompany
                (model.CompanyName, 
                model.CompanyAddressStreetLine1, 
                model.CompanyAddressStreetLine2, 
                model.CompanyAddressCity, 
                model.CompanyAddressState, 
                model.CompanyAddressZipCode);

            return new AjaxResponse
                       {
                           Success = true,
                           Item = company
                       };
        }
    }

    public class AddCompanyInput
    {
        public Core.Domain.Company Company { get; set; } 
        public string CompanyName { get; set; }
        public string CompanyAddressStreetLine1 { get; set; }
        public string CompanyAddressStreetLine2 { get; set; }
        public string CompanyAddressCity { get; set; }
        public string CompanyAddressState { get; set; }
        public string CompanyAddressZipCode { get; set; }
    }
}