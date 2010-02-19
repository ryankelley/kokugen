namespace Kokugen.Core.Domain
{
    public class User : DomainEntity
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string EmailAddress { get; set; }
    }
}