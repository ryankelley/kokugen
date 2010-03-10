using System;
using System.Collections.Generic;

namespace Kokugen.Web.Conventions
{
    public static class ValueObjectRegistry
    {
        public static List<ValueObject> GetAllActive(string name)
        {
            throw new NotImplementedException();
        }

        public static ValueObject FindDefault(string listName)
        {
            throw new NotImplementedException();
        }
    }

    public static class ListNames
    {
        private static List<string> _origins;

        public static List<string> Origins()
        {
            return _origins;
        }
    }
}