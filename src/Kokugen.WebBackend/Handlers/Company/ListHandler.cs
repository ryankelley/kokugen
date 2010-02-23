using System;
using System.Collections.Generic;
using Kokugen.Core.Services;

namespace Kokugen.WebBackend.Handlers.Company
{
    public class ListHandler
    {
        private readonly ICompanyService _companyService;

        public ListHandler(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public CompanyListModel Execute(CompanyListModel model)
        {
            var outModel = new CompanyListModel {Companies = _companyService.ListAllCompanies()};

            return outModel;
        }
    }

    public class CompanyListModel
    {
        public IEnumerable<Core.Domain.Company> Companies { get; set; }

        public string CompanyName { get; set; }
    }
}