using System;
using System.Collections;
using Kokugen.Core.Domain;
using FluentNHibernate.Testing;
using NUnit.Framework;

namespace Kokugen.Tests.Persistence.Tests
{
    [TestFixture]
    public class CompanyPersistenceTester : PersistenceTesterContext<Company>
    {
        [Test]
        public void Test_The_Mappings()
        {
            var address = new Address(){City = "Amarillo", State = "TX", StreetLine1 = "8331 N Western", ZipCode = "52136"};
            Specification
                .CheckProperty(x => x.Name, "Company XYZ")
                .CheckProperty(x => x.Address, address, new TypeComparer())
                .VerifyTheMappings();
        }
    }

    public class TypeComparer : IEqualityComparer 
    {
        public bool Equals(object x, object y)
        {
            return x.GetType() == y.GetType();
        }

        public int GetHashCode(object obj)
        {
            throw new NotImplementedException();
        }
    }

}