using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using Kokugen.Core.Attributes;
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

        private IList<Task> _tasks = new List<Task>();
        private IList<CardActivity> _activities = new List<CardActivity>();

        public virtual int CardNumber { get; set; }

        [MaximumStringLength(10000)]
        public virtual string Details { get; set; }
        public virtual int TimeEstimate { get; set; }
        public virtual int Size { get; set; }
        public virtual string Priority { get; set; }
        public virtual DateTime? Deadline { get; set; }
        public virtual string Color { get; set; }
      
        
        public virtual User AssignedTo { get; set; }


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

        public virtual int CardOrder { get; set; }
        public virtual string BlockReason { get; set; }

        public virtual IEnumerable<Task> GetTasks()
        {
            return _tasks.OrderBy(x => x.TaskOrder);
        }

        public virtual void AddTask(Task task)
        {
            if(_tasks.Contains(task)) return;

            task.Card = this;
            _tasks.Add(task);
        }

        public virtual void RemoveTask(Task task)
        {
            if (_tasks.Contains(task))
                _tasks.Remove(task);
        }

        public virtual IEnumerable<CardActivity> GetActivities()
        {
            return _activities.AsEnumerable();
        }

        public virtual void AddActivity(CardActivity activity)
        {
            if(_activities.Contains(activity)) return;

            activity.Card = this;
            _activities.Add(activity);
        }

        public virtual void RemoveActivity(CardActivity activity)
        {
            if (_activities.Contains(activity))
                _activities.Remove(activity);
        }

        public virtual void StartWorking()
        {
            var lastActivity = _activities.Where(x => x.EndTime == null && x.ActivityId != ActivityType.Column.Value).FirstOrDefault();

            if (lastActivity != null && lastActivity.Status == ActivityType.Idle)
            {
                lastActivity.EndTime = DateTime.Now;
                AddActivity(new CardActivity {StartTime = DateTime.Now, Status = ActivityType.Working});
            }
            else if(lastActivity == null)
                AddActivity(new CardActivity { StartTime = DateTime.Now, Status = ActivityType.Working });
        }

        public virtual void StopActivity()
        {
            var lastActivity = _activities.Where(x => x.EndTime == null && x.ActivityId != ActivityType.Column.Value).FirstOrDefault();
            if (lastActivity != null) lastActivity.EndTime = DateTime.Now;
        }

        public virtual void StartIdle()
        {
            var lastActivity = _activities.Where(x => x.EndTime == null && x.ActivityId != ActivityType.Column.Value).FirstOrDefault();

            if (lastActivity != null && lastActivity.Status == ActivityType.Working)
            {
                lastActivity.EndTime = DateTime.Now;
                AddActivity(new CardActivity { StartTime = DateTime.Now, Status = ActivityType.Idle });
            }
            else if (lastActivity == null)
                AddActivity(new CardActivity { StartTime = DateTime.Now, Status = ActivityType.Idle });
        }

        public virtual void ColumnChanged(BoardColumn oldColumn, BoardColumn newColumn)
        {
            var lastActivity = _activities.Where(x => x.EndTime == null && x.ActivityId == ActivityType.Column.Value).FirstOrDefault();
            if (lastActivity != null) lastActivity.EndTime = DateTime.Now;

            AddActivity(new CardActivity {StartTime = DateTime.Now, Status = ActivityType.Column, Leaving = oldColumn, Entering = newColumn});
        }

        public virtual TimeSpan TotalWorkTime()
        {
            var activities = _activities.Where(x => x.Status == ActivityType.Working && x.EndTime != null).ToList();
            var totalTime = new TimeSpan();
            return activities.Aggregate(totalTime, (current, cardActivity) => current + (cardActivity.EndTime.Value - cardActivity.StartTime));
        }

        public virtual TimeSpan TotalIdleTime()
        {
            var activities = _activities.Where(x => x.Status == ActivityType.Idle && x.EndTime != null).ToList();
            var totalTime = new TimeSpan();
            return activities.Aggregate(totalTime, (current, cardActivity) => current + (cardActivity.EndTime.Value - cardActivity.StartTime));
        }
    }

    public class CardActivity : Entity
    {
        public virtual DateTime StartTime { get; set; }
        public virtual DateTime? EndTime { get; set; }
        public virtual decimal Duration { get; set; }
        public virtual Card Card { get; set; }

        private int _activityId;
        public virtual int ActivityId
        {
            get { return _activityId; }
            set { _activityId = value; }
        }

        public virtual ActivityType Status
        {
            get { return Enumeration.FromValue<ActivityType>(_activityId); }
            set { _activityId = value.Value; }
        }

        public virtual BoardColumn Leaving { get; set; }

        public virtual BoardColumn Entering { get; set; }
    }

    public class ActivityType : Enumeration
    {
        public static ActivityType Working = new ActivityType(1, "Working");
        public static ActivityType Idle = new ActivityType(2, "Idle");
        public static ActivityType Column = new ActivityType(3, "Column");

        private ActivityType(int id, string name) : base(id,name)
        {
            
        }

        public ActivityType()
        {
            
        }
    }

    public class CardStatus : Enumeration
    {
        public static CardStatus New = new CardStatus(1, "New");
        public static CardStatus Ready = new CardStatus(2, "Ready");
        public static CardStatus Blocked = new CardStatus(3, "Blocked");
        public static CardStatus Complete = new CardStatus(4, "Complete");
        private CardStatus(int value, string displayName) : base(value, displayName)
        {
            
        }

        public CardStatus()
        {
            
        }
    }
}