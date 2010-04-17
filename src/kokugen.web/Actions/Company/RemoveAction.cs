using System;
using Kokugen.Core.Services;
using StructureMap;

namespace Kokugen.Web.Actions.Company
{
    public class RemoveAction
    {
        private readonly ICompanyService _companyService;

        public RemoveAction(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public AjaxResponse Remove(RemoveCompanyInput model)
        {
            try
            {
                _companyService.DeleteCompany(model.Id);
                return new AjaxResponse
                           {
                               Success = true,
                               Item = model.Id
                           };
            }
            catch(Exception ex)
            {
                return new AjaxResponse {Success = false, Item = ex.Message};
            }
        }
    }

    public class RemoveCompanyInput
    {
        public Guid Id { get; set; }
    }
}