using System;
using FubuMVC.Core;

namespace Kokugen.Web.Actions.Company
{
    public class CompanyFormAction
    {
        [FubuPartial]
        public CompanyFormModel Execute(CompanyFormModel model)
        {
            var company = new Core.Domain.Company();

            company = model.Company;

            return new CompanyFormModel { Company = company };
        }
    }

    public class CompanyFormModel
    {
        public Core.Domain.Company Company { get; set; }
    }
}