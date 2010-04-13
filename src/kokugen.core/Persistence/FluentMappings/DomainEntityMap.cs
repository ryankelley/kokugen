#region Using Directives

using FluentNHibernate.Mapping;
using Kokugen.Core.Domain;
using Kokugen.Core.Membership;

#endregion

namespace Kokugen.Core.Persistence.FluentMappings
{
    public class DomainEntityMap<ENTITY> : ClassMap<ENTITY>
        where ENTITY : Domain.Entity
    {
        public DomainEntityMap()
        {
            MapEntity();
        }

        private void MapEntity()
        {
            Id(e => e.Id, "Id").GeneratedBy.GuidComb();
        }
    }

    public class DomainEntitySublcassMap<Entity> : SubclassMap<Entity> where Entity : Domain.Entity
    {
        public DomainEntitySublcassMap()
        {
        }
    }

    
}