using System;
using System.Collections.Generic;
using System.Linq;
using Kokugen.Core.Validation;

namespace Kokugen.Core.Domain
{
    [Serializable]
    public class Project : Entity
    {
        private IList<TimeRecord> _timeRecords = new List<TimeRecord>();
        private IList<CustomBoardColumn> _boardColumns = new List<CustomBoardColumn>();
        private IList<User> _users = new List<User>();
        

        [Required]
        public virtual string Name { get; set; }
        public virtual DateTime? StartDate { get; set; }
        public virtual DateTime? EndDate { get; set; }
        public virtual int NumberOfSessions { get; set; }
        public virtual double AverageTimeSpentPerSession { get; set; }
        public virtual double TotalTime { get; set; }

        public virtual string Description { get; set; }
        
        public virtual Company Company { get; set; }
        [Required]
        public virtual User Owner { get; set; }
       

        public virtual BoardColumn Backlog { get; set; }
        public virtual BoardColumn Archive { get; set; }

        public virtual IEnumerable<User> GetUsers()
        {
            return _users;
        }

        public virtual void AddUser(User user)
        {
            _users.Add(user);
        }

        public virtual void RemoveUser(User user)
        {
            if (_users.Contains(user))
                _users.Remove(user);
        }

        #region Time Records

        public virtual IEnumerable<TimeRecord> GetTimeRecords()
        {
            return _timeRecords;
        }

        public virtual void AddTime(TimeRecord timeRecord)
        {
            if(_timeRecords.Contains(timeRecord)) return;

            timeRecord.Project = this;
            _timeRecords.Add(timeRecord);
        }

        public virtual void RemoveTime(TimeRecord timeRecord)
        {
            if(!_timeRecords.Contains(timeRecord)) return;

            _timeRecords.Remove(timeRecord);
        }

        #endregion

        #region Board Columns

        public virtual IEnumerable<BoardColumn> GetAllBoardColumns()
        {
            var output = new List<BoardColumn>();
            output.Add(Backlog);
            output.AddRange(GetBoardColumns().OrderBy(x => x.ColumnOrder).Select(x => x as BoardColumn));
            output.Add(Archive);

            return output;
        }

        public virtual IEnumerable<CustomBoardColumn> GetBoardColumns()
        {
            return _boardColumns.AsEnumerable();
        }

        public virtual void AddBoardColumn(CustomBoardColumn column)
        {
            if(_boardColumns.Contains(column)) return;
            column.Project = this;
            _boardColumns.Add(column);
        }

        public virtual void RemoveBoardColumn(CustomBoardColumn column)
        {
            if (_boardColumns.Contains(column))
                _boardColumns.Remove(column);
        }

        #endregion

        private IList<Card> _cards = new List<Card>();

        public virtual IEnumerable<Card> GetCards()
        {
            return _cards.AsEnumerable();
        }

        public virtual void AddCard(Card card)
        {
            if (_cards.Contains(card)) return;

            card.Project = this;
            _cards.Add(card);
        }

        public virtual void RemoveCard(Card card)
        {
            if (_cards.Contains(card))
                _cards.Remove(card);
        }

        public virtual void ComputeTotalTimeAndSessions()
        {
            int i = 0;
            foreach (var timeRecord in _timeRecords)
            {
                ++i;
                TotalTime += timeRecord.Duration;
            }
            NumberOfSessions = i;
        }

        public virtual void ComputeAverageTimeSpentPerSession()
        {
            ComputeTotalTimeAndSessions();

            AverageTimeSpentPerSession = TotalTime/(Double)NumberOfSessions;
            
        }

    }
}