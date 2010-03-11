using System;
using Kokugen.Web.Conventions;
using Kokugen.Core.Attributes;

namespace Kokugen.Web.Actions.Project
{
    public class AddProjectModel
    {
        public Core.Domain.Project Project { get; set; }
        public Guid CompanyId { get; set; }
    }
}