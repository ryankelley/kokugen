using Kokugen.Core.Attributes;
using Kokugen.Core.Validation;

namespace Kokugen.Core.Domain
{
    public class Company : Entity
    {
        [Required, ValueOf()]
        public string Name { get; set; }
        
    }
}