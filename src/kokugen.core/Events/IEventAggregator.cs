using System;

namespace Kokugen.Core.Events
{
    public interface IListener
    {
    }

    public interface IListener<T>
    {
        void Handle(T message);
    }

    public interface IEventAggregator
    {
        // Sending messages
        void SendMessage<T>(T message);
        void SendMessage<T>() where T : new();

        // This method sounded cool, but has been somewhat awkward
        // in real usage
        void SendMessage<T>(Action<T> action) where T : class;

        // Explicit registration
        //void AddListener(object listener);

        void AddListener(Type listenerType);

        void RemoveListener(object listener);

        // Filtered registration, experimental
        If<T> If<T>(Func<T, bool> filter);
    }

    public interface If<T>
    {
        object PublishTo(Action<T> action);
    }
}