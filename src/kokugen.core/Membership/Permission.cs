using Kokugen.Core.Domain;

namespace Kokugen.Core.Membership
{
    public class Permission : Enumeration
    {
        public static Permission CanViewProject = new Permission(1, "Can View Project");
        public static Permission CanListProjects = new Permission(2, "Can List Projects");
        public static Permission CanConfigureProcess = new Permission(3, "Can Configure Process");

        private Permission(int value, string displayName) : base(value, displayName)
        {
            
        }

        public Permission()
        {
            
        }
    }

    
}