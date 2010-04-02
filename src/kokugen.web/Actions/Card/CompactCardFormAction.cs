using System;
using FubuMVC.Core;

namespace Kokugen.Web.Actions.Card
{
    public class CompactCardFormAction
    {
        [FubuPartial]
        public CompactCardFormModel CompactCardForm(CompactCardFormInput model)
        {
            return new CompactCardFormModel {ProjectId = model.Id};
        }
    }

    public class CompactCardFormInput
    {
        public Guid Id { get; set; }
    }

    public class CompactCardFormModel
    {
        public Guid ProjectId { get; set; }
        public Core.Domain.Card Card { get; set; }
    }
}