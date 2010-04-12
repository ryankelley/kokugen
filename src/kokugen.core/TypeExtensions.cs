using System;
using System.Collections;
using System.Collections.Generic;
using FluentNHibernate;
using FubuCore.Binding;
#if !DEBUG
using System.Linq;
using FubuCore.Reflection;
using Kokugen.Core.Attributes;
#endif
using Kokugen.Core.Persistence;
using StructureMap;
using StructureMap.Pipeline;

namespace Kokugen.Core
{
    public static class StructureMapExtensions
    {
        public static void ExecuteInTransaction(this IContainer container, Action<IContainer> action)
        {
            var transactionProcessor = new TransactionProcessor(container);
            transactionProcessor.WithinTransaction(action);
            
        }

        public static void StartStartables(this IContainer container)
        {
#if DEBUG
            //container.GetInstance<ISessionSource>().BuildSchema();   
#endif
            container.ExecuteInTransaction(c => c.Model.GetAllPossible<IStartable>()
#if !DEBUG
                        .Where(x => !x.GetType().HasAttribute<DebugOnlyAttribute>())
#endif
                                                    .Each(x => x.Start()));
            
        }
    }

    public static class ServiceArgumentsExtensions
    {
        public static ExplicitArguments ToExplicitArgs(this ServiceArguments arguments)
        {
            var explicits = new ExplicitArguments();
            arguments.EachService(explicits.Set);

            return explicits;
        }
    }

    public static class TypeExtensions
    {
        public static bool Is<T>(this object target)
        {
            return typeof(T).IsAssignableFrom(target.GetType());
        }

        public static bool IsType<T>(this Type type)
        {
            return type == typeof (T);
        }

        public static void Add<T>(this List<Type> list)
        {
            list.Add(typeof(T));
        }

        public static void CallOn<T>(this object target, Action<T> action) where T : class
        {
            var subject = target as T;
            if (subject != null)
            {
                try
                {
                    action(subject);
                }
                catch (InvalidOperationException e)
                {
                    if (!e.ToString().Contains("The calling thread"))
                    {
                        throw;
                    }
                }
            }
        }

        public static void CallOnEach<T>(this IEnumerable enumerable, Action<T> action) where T : class
        {
            foreach (object o in enumerable)
            {
                o.CallOn(action);
            }
        }

        
    }
}