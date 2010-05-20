using System;
using System.Linq.Expressions;
using Kokugen.Core.Membership;
using Kokugen.Web.Actions.Board;
using Kokugen.Web.Actions.Board.Configure;
using Kokugen.Web.Actions.Company;
using Kokugen.Web.Actions.DailyTimeRecord;
using Kokugen.Web.Actions.Project;
using Kokugen.Web.Actions.TaskCategory;
using Kokugen.Web.Actions.TimeRecord;
using Kokugen.Web.Actions.Users;

namespace Kokugen.Web
{
    public class KokugenSecurityRegistry : SecurityRegistry
    {
        public KokugenSecurityRegistry()
        {
            For<ListAction>(c => c.Query(null))
                .RequirePermission(PermissionName.CanListProjects);

            For<ConfigureAction>(c => c.Query(null))
                .RequirePermission(PermissionName.CanConfigureProcess);

            For<DailyTimeListAction>(c => c.Query(null))
                .RequirePermission(PermissionName.CanListDailyTime);

            For<TimeRecordListAction>(c => c.Query(null))
                .RequirePermission(PermissionName.CanListTimeRecords);
           
            For<TaskListAction>(c => c.Query(null))
                .RequirePermission(PermissionName.CanListTasks);

            For<UserListAction>(c => c.Query(null))
                .RequirePermission(PermissionName.CanListUsers);

            For<CompanyListAction>(c => c.Query(null)).RequirePermission(PermissionName.CanListCompanies);
            
        }

        
    }
}