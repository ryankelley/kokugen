using System;

namespace Kokugen.Core.Attributes
{
    public class ValueOfAttribute : Attribute
    {
        public ValueOfAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }

    
}