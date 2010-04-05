using System;
using System.Collections.Generic;
using System.Linq;
using Kokugen.Core.Validation;

namespace Kokugen.Core.Domain
{
    

    [Serializable]
    public class BoardColumn : Entity
    {
        [Required]
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual int Limit { get; set; }

        private IList<Card> _cards = new List<Card>();

        public virtual IEnumerable<Card> GetCards()
        {
            return _cards.AsEnumerable();
        }

        public virtual void AddCard(Card card)
        {
            if (_cards.Contains(card)) return;

            card.Column = this;
            _cards.Add(card);
        }

        public virtual void RemoveCard(Card card)
        {
            if(_cards.Contains(card))
                _cards.Remove(card);
        }
    }

    [Serializable]
    public class CustomBoardColumn : BoardColumn
    {
        
        public virtual int ColumnOrder { get; set; }
        public virtual Project Project { get; set; }
    }

}