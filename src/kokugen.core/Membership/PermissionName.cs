using Kokugen.Core.Domain;

namespace Kokugen.Core.Membership
{
    public class PermissionName : Enumeration
    {
        public static PermissionName CanViewProject = new PermissionName(1, "CanViewProject");
        public static PermissionName CanListProjects = new PermissionName(2, "CanListProjects");
        public static PermissionName CanConfigureProcess = new PermissionName(3, "CanConfigureProcess");
        public static PermissionName CanListDailyTime = new PermissionName(4, "CanListDailyTime");
        public static PermissionName CanListTimeRecords = new PermissionName(5,"CanListTimeRecords");
        public static PermissionName CanListTasks = new PermissionName(6,"CanListTasks");
        public static PermissionName CanListUsers = new PermissionName(7, "CanListUsers");
        public static PermissionName CanListCompanies = new PermissionName(8, "CanListCompanies");

        private PermissionName(int value, string displayName) : base(value, displayName)
        {
            
        }

        public PermissionName()
        {
            
        }
    }

    
}