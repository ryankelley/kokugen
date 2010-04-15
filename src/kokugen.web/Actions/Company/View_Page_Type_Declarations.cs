using FubuMVC.Core.View;
using Kokugen.Web.Actions.Project;

namespace Kokugen.Web.Actions.Company
{
    public class List : FubuPage<CompanyListModel> { }
    public class CompanyForm : FubuPage<CompanyFormModel> { }
    public class CompanyItem_Control : FubuControl<Core.Domain.Company>{}
}