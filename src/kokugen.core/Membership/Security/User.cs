using System.Collections.Generic;
using System.Linq;
using Kokugen.Core.Domain;
using Kokugen.Core.Validation;

namespace Kokugen.Core.Membership.Security
{
    public class User : Entity
    {

        [Required]
        public virtual string FirstName { get; set; }
        
        [Required]
        public virtual string LastName { get; set; }

        [Required]
        public virtual string HashedPassword { get; set; }
        
        [Required]
        public virtual string Login { get; set; }   

        [Required, ValidEmail]
        public virtual string EmailAddress { get; set; }

       
    }
}