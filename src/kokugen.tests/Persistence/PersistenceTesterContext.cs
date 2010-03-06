using System.Collections;
using FluentNHibernate.Testing;
using Kokugen.Core;
using Kokugen.Core.Domain;
using NUnit.Framework;
using ISessionSource=FluentNHibernate.ISessionSource;

namespace Kokugen.Tests.Persistence
{
    public class PersistenceTesterContext<ENTITY>
        where ENTITY : Entity, new()
    {
        private readonly IEqualityComparer _equalityComparer;
        private ISessionSource _source;
        private bool _useEqualityComparer;

        [SetUp]
        public void SetUp()
        {
            _source =IntegrationAssemblySetup.SessionSource;
        }

        public PersistenceTesterContext()
        {
            _useEqualityComparer = false;
        }

        public PersistenceTesterContext(IEqualityComparer equalityComparer)
        {
            _useEqualityComparer = true;
            _equalityComparer = equalityComparer;
        }

        public PersistenceSpecification<ENTITY> Specification
        {
            get
            {
                if (_useEqualityComparer) return new PersistenceSpecification<ENTITY>(_source, _equalityComparer);
                    
                return new PersistenceSpecification<ENTITY>(_source);
            }
        }
    }
}