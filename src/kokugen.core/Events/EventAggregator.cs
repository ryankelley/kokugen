using System;
using System.Collections.Generic;

namespace Kokugen.Core.Events
{
    public class SimpleEventAggregator : EventAggregator
    {
        public SimpleEventAggregator()
            : base(new IHandler[0])
        {
        }

        protected override void sendAction(Action action)
        {
            action();
        }
    }

    public class EventAggregator : IEventAggregator
    {
        //private readonly SynchronizationContext _context;
        private readonly List<object> _listeners = new List<object>();
        private readonly List<Type> _typeListeners = new List<Type>();

        private readonly object _locker = new object();

        public EventAggregator(IHandler[] handlers)
        {
            //_context = context;
            _listeners.AddRange(handlers);
        }

        #region IEventAggregator Members

        public void SendMessage<T>(Action<T> action) where T : class
        {
            sendAction(() => all().Each(x => x.CallOn(action)));
        }

        public void SendMessage<T>(T message)
        {
            sendAction(() => all().CallOnEach<IListener<T>>(x => { x.Handle(message); }));
        }

        public void SendMessage<T>() where T : new()
        {
            SendMessage(new T());
        }

        public void AddListener(Type type)
        {
            if(!_typeListeners.Contains(type))
                _typeListeners.Add(type);
        }

        public void AddListener(object listener)
        {
            withinLock(() =>
            {
                if (_listeners.Contains(listener)) return;
                _listeners.Add(listener);
            });
        }

        public void RemoveListener(object listener)
        {
            withinLock(() => _listeners.Remove(listener));
        }

        public If<T> If<T>(Func<T, bool> filter)
        {
            return new IfExpression<T>(filter, this);
        }

        #endregion

        private object[] all()
        {
            lock (_locker)
            {
                return _typeListeners.ToArray();
            }
        }

        private void withinLock(Action action)
        {
            lock (_locker)
            {
                action();
            }
        }

        protected virtual void sendAction(Action action)
        {
            action();
            //_context.Send(state => { action(); }, null);
        }

        public void AddListeners(params object[] listeners)
        {
            foreach (object listener in listeners)
            {
                AddListener(listener);
            }
        }

        public bool HasListener(object listener)
        {
            return _listeners.Contains(listener);
        }

        public void RemoveAllListeners()
        {
            _listeners.Clear();
        }

        public void RemoveAllListeners(Predicate<object> filter)
        {
            _listeners.RemoveAll(filter);
        }

        #region Nested type: IfExpression

        internal class IfExpression<T> : If<T>
        {
            private readonly EventAggregator _aggregator;
            private readonly Func<T, bool> _filter;

            public IfExpression(Func<T, bool> filter, EventAggregator aggregator)
            {
                _filter = filter;
                _aggregator = aggregator;
            }

            #region If<T> Members

            public object PublishTo(Action<T> action)
            {
                var listener = new FilteredListener<T>(_filter, action);
                _aggregator.AddListener(listener);

                return listener;
            }

            #endregion
        }

        #endregion
    }
}