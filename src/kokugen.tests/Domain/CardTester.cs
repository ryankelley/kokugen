using System;
using System.Linq;
using Kokugen.Core.Domain;
using NUnit.Framework;
using Rhino.Mocks;
using StructureMap.AutoMocking;

namespace Kokugen.Tests.Domain
{
    [TestFixture]
    public class CardTester : ContextSpecification
    {
        protected Card _card;
        private RhinoAutoMocker<Card> _mocks;

        protected override void SetContext()
        {
        }

        
    }
}