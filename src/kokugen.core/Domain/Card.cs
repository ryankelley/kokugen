using System;
using System.Web.Security;
using Kokugen.Core.Domain;
using Kokugen.Core.Membership.Security;
using Kokugen.Core.Validation;

namespace Kokugen.Core.Domain
{
    [Serializable]
    public class Card : Entity
    {
        [Required, MaximumStringLength(2047)]
        public virtual string Title { get; set; }

        public virtual int CardNumber { get; set; }

        public virtual string Details { get; set; }
        public virtual int TimeEstimate { get; set; }
        public virtual int Size { get; set; }
        public virtual string Priority { get; set; }
        public virtual DateTime? Deadline { get; set; }
        public virtual string Color { get; set; }
        public virtual Guid AssignedTo { get; set; }


        //Dates
        public virtual DateTime? Started { get; set; }
        public virtual DateTime? DateCompleted { get; set; }

        [Required]
        public virtual Project Project { get; set; }

        private int _statusId;
        public virtual int StatusId
        {
            get { return _statusId; }
            set { _statusId = value;}
        }

        public virtual CardStatus Status
        {
            get { return Enumeration.FromValue<CardStatus>(_statusId); }
            set { _statusId = value.Value; }
        }

        public virtual BoardColumn Column { get; set; }

        public virtual string BlockReason { get; set; }
    }

    public class CardStatus : Enumeration
    {
        public static CardStatus New = new CardStatus(1, "New");
        public static CardStatus Ready = new CardStatus(2, "Ready");
        public static CardStatus Blocked = new CardStatus(3, "Blocked");
        private CardStatus(int value, string displayName) : base(value, displayName)
        {
            
        }

        public CardStatus()
        {
            
        }
    }
}