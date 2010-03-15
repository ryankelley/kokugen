using System;
using FubuMVC.UI.Configuration;

namespace Kokugen.Web.Conventions
{
    public static class ValueObjectDropdownExtensions 
    {
        
        public static void ForListName(this ElementRequest request, Action<string> action)
        {
            request.ForValue(action);
        }

        public static void EachValueObject(this ElementRequest request, Action<ValueObject> tag )
        {
            request.ForValue(tag);
        }
    }

}