using System;
using System.Web.Script.Serialization;
using Kokugen.Core;
using Kokugen.Core.Domain;
using Kokugen.Core.Services;
using Kokugen.Web.Conventions;
using System.Collections.Generic;

namespace Kokugen.Web.Actions.Card
{
    public class CardDateInputModel 
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
    }

    public class CardOrderInputModel
    {
        public string Cards { get; set; }
    }

    public class CardBlockedInput
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
    }

    public class CardReadyInput
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
    }

    public class CardColorChangeInput
    {
        public Guid Id { get; set; }
        public string Color { get; set; }
    }

    public class CardMoveInputModel
    {
        public Guid Id { get; set; }
        public Guid ColumnId { get; set; }
    }
}