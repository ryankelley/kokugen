using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FubuCore.Reflection;
using FubuCore.Util;
using FubuMVC.Core.Urls;

namespace Kokugen.Core.Membership
{
    public class SecurityDataHolder
    {
        public SecurityDataHolder(Type handlerType, MethodInfo method)
        {
            HandlerType = handlerType;
            ActionCall = method;
        }

        public Type HandlerType { get; set; }
        public MethodInfo ActionCall { get; set; }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var data = obj as SecurityDataHolder;

            return (HandlerType.Equals(data.HandlerType) && ActionCall.Equals(data.ActionCall));

        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return ((HandlerType != null ? HandlerType.GetHashCode() : 0) * 256) ^
                       (ActionCall != null ? ActionCall.GetHashCode() : 0);
        }

    }

    public class SecurityRegistry
    {

        private Cache<SecurityDataHolder, SecurityConfigExpression> _securityInfo = new Cache<SecurityDataHolder, SecurityConfigExpression>();

        protected SecurityConfigExpression For<T>(Expression<Func<T, object>> call)
        {
            var method = ReflectionHelper.GetMethod(call);

            var data = new SecurityDataHolder(typeof(T), method);

            if (_securityInfo.Has(data))
                return _securityInfo[data];

            var output = new SecurityConfigExpression(method);
            _securityInfo.Fill(data, output);
            return output;
        }

        public Cache<SecurityDataHolder, SecurityConfigExpression> GetRegisteredMembers()
        {
            return _securityInfo;
        }
    }

    public class SecurityProvider
    {
        private static Cache<SecurityDataHolder, SecurityConfigExpression> _securityInfo = new Cache<SecurityDataHolder, SecurityConfigExpression>();


        public static void Configure(SecurityRegistry registry)
        {
            registry.GetRegisteredMembers().Each((key, value) => _securityInfo.Fill(key, value));
        }

        public static bool HasPermissionForMethod(Type handlerType, MethodInfo method)
        {
            return _securityInfo.Has(makeKey(handlerType, method));
        }

        public static IEnumerable<Permission> GetPermissionsForMethod(Type handlerType, MethodInfo method)
        {
            return _securityInfo[makeKey(handlerType, method)].GetPermissions();
        }

        private static SecurityDataHolder makeKey(Type handlerType, MethodInfo method)
        {
            return new SecurityDataHolder(handlerType, method);
        }
    }

    



    public class SecurityConfigExpression
    {
        private readonly IList<Permission> _permissions = new List<Permission>();

        public SecurityConfigExpression(MethodInfo methodInfo)
        {
            this.MethodInfo = methodInfo;
        }

        protected MethodInfo MethodInfo { get; set; }

        public SecurityConfigExpression RequirePermission(Permission permission)
        {
            _permissions.Add(permission);
            return this;
        }

        public IEnumerable<Permission> GetPermissions()
        {
            return _permissions.AsEnumerable();
        }
    }
}