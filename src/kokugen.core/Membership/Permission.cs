using Kokugen.Core.Domain;

namespace Kokugen.Core.Membership
{
    public class Permission : Enumeration
    {
        public static Permission CanViewProject = new Permission(1, "CanViewProject");
        public static Permission CanListProjects = new Permission(2, "CanListProjects");
        public static Permission CanConfigureProcess = new Permission(3, "CanConfigureProcess");

        private Permission(int value, string displayName) : base(value, displayName)
        {
            
        }

        public Permission()
        {
            
        }
    }

    
}