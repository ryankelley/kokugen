#region Using Directives

using System.Collections.Generic;
using System.Reflection;

#endregion

namespace Kokugen.Core
{
    public class PropertyComparer : IEqualityComparer<PropertyInfo>
    {
        #region IEqualityComparer<PropertyInfo> Members

        public bool Equals(PropertyInfo x, PropertyInfo y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(PropertyInfo obj)
        {
            return obj.GetHashCode();
        }

        #endregion
    }
}