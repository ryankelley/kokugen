using Kokugen.Core.Validation;

namespace Kokugen.Core.Domain
{
    public class User : Entity
    {
        [Required]
        public virtual string FirstName { get; set; }
        
        [Required]
        public virtual string LastName { get; set; }
        [Required, ValidEmail]
        public virtual string EmailAddress { get; set; }
    }
}