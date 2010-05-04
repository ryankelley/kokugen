using System;
using System.Linq;
using AutoMapper;
using FubuMVC.Core.Urls;
using Kokugen.Core;
using Kokugen.Core.Domain;
using Kokugen.Core.Services;
using Kokugen.Web.Actions.Board;
using Kokugen.Web.Actions.Card;
using Kokugen.Web.Actions.Project.Manage.Users;
using Kokugen.Web.Actions.TimeRecord.WidgetLog;

namespace Kokugen.Web.Startables
{
    public class AutomapperConfigurationStartable : IStartable 
    {
        private readonly IUrlRegistry _urlRegistry;

        public AutomapperConfigurationStartable(IUrlRegistry urlRegistry)
        {
            _urlRegistry = urlRegistry;
        }

        public void Start()
        {
            Mapper.CreateMap<Card, CardViewDTO>()
               .ForMember(a => a.Status, b => b.MapFrom(c => c.Status.DisplayName))
               .ForMember(a => a.GravatarHash, b => b.MapFrom(c => c.AssignedTo.GravatarHash))
               .ForMember(a => a.UserDisplay, b => b.MapFrom(c => c.AssignedTo.DisplayName()))
               .ForMember(a => a.GetTasks, b => b.MapFrom(c => c.GetTasks().ToList()));
            Mapper.CreateMap<Card, CardDetailModel>()
                .ForMember(a => a.Status, b => b.MapFrom(c => c.Status.DisplayName))
                .ForMember(a => a.GravatarHash, b => b.MapFrom(c => c.AssignedTo.GravatarHash))
                .ForMember(a => a.UserDisplay, b => b.MapFrom(c => c.AssignedTo.DisplayName()))
                .ForMember(a => a.GetTasks, b => b.MapFrom(c => c.GetTasks().ToList()));
            Mapper.CreateMap<BoardColumn, BoardColumnDTO>()
                .ForMember(a => a.CardLimit, b => b.UseValue(0));
            Mapper.CreateMap<CustomBoardColumn, BoardColumnDTO>()
                .ForMember(a => a.CardLimit, b => b.NullSubstitute(0));

            Mapper.CreateMap<TimeRecord, TimeLogItem>();
            Mapper.CreateMap<Task, TaskDTO>();
            //            Mapper.CreateMap<TimeRecord, TimeRecordDTO>()
            //                .ForMember(a => a.User, b => b.NullSubstitute(null));

            Mapper.CreateMap<User, ProjectUserDTO>()
                .ForMember(x => x.IsOwner, map => map.Ignore());

            Mapper.AssertConfigurationIsValid();
        }
    }
}