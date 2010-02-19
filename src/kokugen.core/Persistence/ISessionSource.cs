using NHibernate;

namespace Kokugen.Core.Persistence
{
    public interface ISessionSource
    {
        ISession CreateSession();
        void BuildSchema();

    }
}