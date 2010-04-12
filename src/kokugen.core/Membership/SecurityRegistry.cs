using System;
using System.Collections.Generic;
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

    public class SecurityProvider : ISecurityProvider
    {
        private static Cache<string, SecurityConfigExpression> _securityInfo = new Cache<string, SecurityConfigExpression>();

        private readonly IUrlRegistry _urlRegistry;


        public SecurityProvider(IUrlRegistry urlRegistry)
        {
            _urlRegistry = urlRegistry;
        }

        public void Configure(SecurityRegistry registry)
        {
            registry.GetRegisteredMembers().Each((key, value) =>
                                                     {
                                                         var url = _urlRegistry.UrlFor(key.HandlerType, key.ActionCall);
                                                         _securityInfo.Fill(url, value);
                                                     });
        }

        public bool HasPermissionForUrl(string url)
        {
            return false;
        }
    }

    public interface ISecurityProvider
    {
        void Configure(SecurityRegistry registry);
        bool HasPermissionForUrl(string url);
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