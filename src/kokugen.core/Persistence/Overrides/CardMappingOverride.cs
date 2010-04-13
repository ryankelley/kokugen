using System;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Kokugen.Core.Domain;

namespace Kokugen.Core.Persistence.Overrides
{
    public class CardMappingOverride : IAutoMappingOverride<Card>
    {
        public void Override(AutoMapping<Card> mapping)
        {
            mapping.Map(x => x.CardNumber).Generated.Always().Not.Update().Not.Insert().Not.LazyLoad().ReadOnly();
            mapping.References(x => x.Column, "Column_id").Cascade.SaveUpdate().ForeignKey("fk_card_to_column");
            mapping.References(x => x.Project).Cascade.SaveUpdate().ForeignKey("fk_card_to_project");
        }
    }
}