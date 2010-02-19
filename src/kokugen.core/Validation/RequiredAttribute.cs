#region Using Directives

using System;
using System.Reflection;

#endregion

namespace Kokugen.Core.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class RequiredAttribute : ValidationAttribute
    {
        public static Func<RequiredAttribute, PropertyInfo, string> GetMessage = (att, prop) =>
                                                                                     {
                                                                                         if (!string.IsNullOrEmpty(att.Message))
                                                                                         {
                                                                                             return att.Message;
                                                                                         }

                                                                                         return Notification.REQUIRED_FIELD;
                                                                                     };

        public string Message { get; set; }

        protected override void validate(object target, object rawValue, INotification notification)
        {
            if (rawValue is DateTime)
            {
                if ((DateTime) rawValue == new DateTime())
                    logMessage(notification, GetMessage(this, Property));
                return;
            }

            if(rawValue is decimal)
            {
                if((decimal) rawValue == 0)
                    logMessage(notification, GetMessage(this,Property));
                return;
            }

            if (rawValue == null || (string) rawValue == string.Empty)
            {
                logMessage(notification, GetMessage(this, Property));
            }
        }

        public static bool IsRequired(PropertyInfo property)
        {
            var attribute = GetCustomAttribute(property, typeof (RequiredAttribute)) as RequiredAttribute;
            return attribute != null && attribute.GetType().Equals(typeof (RequiredAttribute));
        }
    }
}