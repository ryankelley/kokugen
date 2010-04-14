using System;

namespace Kokugen.Core
{
    public class ValueObject
    {
        public string Key { get; private set; }
        public string Value { get; private set; }

        public bool IsDefault { get; set; }

        public ValueObject(string key)
        {
            Key = key;
            Value = key;
        }

        public ValueObject(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string LocalizedText ()
        {
            return Value;
        }

        public override bool Equals(object obj)
        {
            if (obj is ValueObject)
            {
                var compare = obj as ValueObject;
                return this.Key.Equals(compare.Key);
            }
            return false;
        }
    }
}