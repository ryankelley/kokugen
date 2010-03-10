using Kokugen.Core.Validation;

namespace Kokugen.Core.Domain
{
    public class Address : Entity
    {
        [Required]
        public virtual string StreetLine1 { get; set; }
        public virtual string StreetLine2 { get; set; }
        [Required]
        public virtual string City { get; set; }
        [Required]
        public virtual string State { get; set; }
        [Required]
        public virtual string ZipCode { get; set; }
    }
}