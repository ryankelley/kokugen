using Kokugen.Core.Domain;
using Kokugen.Core.Events;
using Kokugen.Core.Events.Messages;
using NHibernate;

namespace Kokugen.Core.Persistence.Repositories
{
    public class PublishingRepository<T> : NHibernateRepository<T> where T : Entity
    {
        private readonly IEventAggregator _eventAggregator;

        public PublishingRepository(ISession session, IEventAggregator eventAggregator) : base(session)
        {
            _eventAggregator = eventAggregator;
        }

        public override void Save(T entity)
        {
            base.Save(entity);
            _eventAggregator.SendMessage(new ValueEntitySaved<T>(entity));
        }

        public override void SaveAndFlush(T entity)
        {
            base.SaveAndFlush(entity);
            _eventAggregator.SendMessage(new ValueEntitySaved<T>(entity));
        }

        public override void Delete(T entity)
        {
            base.Delete(entity);
            _eventAggregator.SendMessage(new ValueEntityRemoved<T>(entity));
        }
    }
}