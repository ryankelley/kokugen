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
        public CompanyPersistenceTester() : base(new AddressEqualityComparer())
        {
        }

        [Test]
        public void Test_The_Mappings()
        {
            var address = new Address(){City = "Amarillo", State = "TX", StreetLine1 = "8331 N Western", ZipCode = "52136"};
            Specification
                .CheckProperty(x => x.Name, "Company XYZ")
                .CheckProperty(x => x.Address, address, new TypeComparer())
                .CheckProperty(x => x.Address, address)
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

    public class AddressEqualityComparer : IEqualityComparer
    {
        public bool Equals(object x, object y)
        {
            if (x == null || y == null)
                return false;
            if (x is Address && y is Address)
                return ((Address) x).StreetLine1 == ((Address) y).StreetLine1;
            return x.Equals(y);
        }

        public int GetHashCode(object obj)
        {
            throw new NotImplementedException();
        }
    }

}