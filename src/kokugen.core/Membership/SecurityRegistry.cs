using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using FubuCore.Reflection;
using FubuCore.Util;

namespace Kokugen.Core.Membership
{
    public class SecurityRegistry
    {
        private static Cache<MethodInfo, SecurityConfigExpression> _securityInfo = new Cache<MethodInfo, SecurityConfigExpression>();

        protected SecurityConfigExpression For<T>(Expression<Func<T, object>> call)
        {
            var method = ReflectionHelper.GetMethod(call);

            if (_securityInfo.Has(method))
                return _securityInfo[method];

            var output = new SecurityConfigExpression(method);
            _securityInfo.Fill(method, output);
            return output;
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
    }
}