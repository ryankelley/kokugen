using System.Collections.Generic;
using Kokugen.Core.Domain;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.Company
{
    public class CompanyListAction
    {
        private readonly ICompanyService _companyService;

        public CompanyListAction(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public CompanyListModel Query(CompanyListModel model)
        {
            var outModel = new CompanyListModel {Companies = _companyService.ListAllCompanies()};

            return outModel;
        }
    }

    public class CompanyListModel
    {
        public IEnumerable<Core.Domain.Company> Companies { get; set; }

        public Core.Domain.Company Company { get; set; }
    }
}