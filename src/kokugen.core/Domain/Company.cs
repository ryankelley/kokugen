using Kokugen.Core.Attributes;
using Kokugen.Core.Validation;


namespace Kokugen.Core.Domain
{
    public class Company : Entity
    {
        [Required, ValueOf()]
        public virtual string Name { get; set; }
        public virtual Address Address { get; set; }
    }
}