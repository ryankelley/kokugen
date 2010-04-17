using System;
using Kokugen.Core.Events;
using StructureMap;
using StructureMap.Interceptors;
using StructureMap.TypeRules;

namespace Kokugen.Core
{
    public class EventAggregatorInterceptor : TypeInterceptor
    {
        #region TypeInterceptor Members

        public object Process(object target, IContext context)
        {
            context.GetInstance<IEventAggregator>().AddListener(target);
            return target;
        }

        public bool MatchesType(Type type)
        {
            if (typeof(IHandler).IsAssignableFrom(type)) return false;


            return
                type.ImplementsInterfaceTemplate(typeof(IListener<>));
        }

        #endregion
    }
}