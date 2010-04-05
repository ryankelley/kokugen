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
            mapping.Map(x => x.CardNumber).Generated.Always();
            mapping.References(x => x.Column).Cascade.SaveUpdate().ForeignKey("fk_card_to_column");
        }
    }
}