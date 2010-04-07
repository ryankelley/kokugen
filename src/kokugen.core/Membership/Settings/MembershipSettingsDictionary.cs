using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Security;
using FubuCore.Binding;
using FubuCore.Reflection;

namespace Kokugen.Core.Membership.Settings
{
    public class MembershipSettingsDictionary : IRequestData
    {
        private readonly MembershipProvider _provider;
        private  readonly IDictionary<string,object> _settings = new Dictionary<string, object>();

        public MembershipSettingsDictionary(MembershipProvider provider)
        {
            _provider = provider;

            AddProperty(x => x.ApplicationName);
            AddProperty(x => x.Description);
            AddProperty(x => x.EnablePasswordReset);
            AddProperty(x => x.EnablePasswordRetrieval);
            AddProperty(x => x.MaxInvalidPasswordAttempts);
            AddProperty(x => x.MinRequiredNonAlphanumericCharacters);
            AddProperty(x => x.MinRequiredPasswordLength);
            AddProperty(x => x.PasswordAttemptWindow);
            AddProperty(x => x.PasswordFormat);
            AddProperty(x => x.PasswordStrengthRegularExpression);
            AddProperty(x => x.RequiresQuestionAndAnswer);
            AddProperty(x => x.RequiresUniqueEmail);
            
        }

        private void AddProperty(Expression<Func<MembershipProvider, object>> expression)
        {
            var accessor = expression.ToAccessor();
            _settings.Add(accessor.FieldName,accessor.GetValue(_provider));
        }

        public object Value(string key)
        {
            return _settings[key];
        }

        public bool Value(string key, Action<object> callback)
        {
            if(_settings.ContainsKey(key))
            {
                callback(Value(key));
                return true;
            }
            return false;
        }
    }
}