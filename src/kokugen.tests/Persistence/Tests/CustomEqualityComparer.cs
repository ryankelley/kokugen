using System;
using System.Collections;
using Kokugen.Core.Domain;

namespace Kokugen.Tests.Persistence.Tests
{
    public class CustomEqualityComparer : IEqualityComparer
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