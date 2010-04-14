using Kokugen.Core.Domain;

namespace Kokugen.Core.Membership
{
    public class PermissionName : Enumeration
    {
        public static PermissionName CanViewProject = new PermissionName(1, "CanViewProject");
        public static PermissionName CanListProjects = new PermissionName(2, "CanListProjects");
        public static PermissionName CanConfigureProcess = new PermissionName(3, "CanConfigureProcess");

        private PermissionName(int value, string displayName) : base(value, displayName)
        {
            
        }

        public PermissionName()
        {
            
        }
    }

    
}