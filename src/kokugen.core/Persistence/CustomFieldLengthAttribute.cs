using System;

namespace Kokugen.Core.Persistence
{
    /// <summary>
    /// This class is used to specify a value for the string length use in Fluent NH Mapping Convention.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CustomFieldLengthAttribute : Attribute
    {
        private readonly int _length;

        public CustomFieldLengthAttribute(int length)
        {
            _length = length;
        }

        public int Length
        {
            get { return _length; }
        }
    }
}