using System;

namespace Kokugen.Web.Conventions
{
    public class ValueObject
    {
        public string Key { get; private set; }
        public string Value { get; private set; }

        public bool IsDefault { get; set; }

        public ValueObject(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string LocalizedText ()
        {
            return Value;
        }
    }
}