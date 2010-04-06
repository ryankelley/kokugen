using System;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Mapping;
using Kokugen.Core.Domain;

namespace Kokugen.Core.Persistence.Overrides
{
    public class BoardColumnMappingOverride : IAutoMappingOverride<BoardColumn>
    {
        public void Override(AutoMapping<BoardColumn> mapping)
        {
            mapping.Map(x => x.Limit, "[Limit]");
        }
    }
}