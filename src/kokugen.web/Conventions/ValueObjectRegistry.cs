using System;
using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core.Util;
using Kokugen.Core.Domain;
using Kokugen.Core.Services;

namespace Kokugen.Web.Conventions
{
    public static class ValueObjectRegistry
    {
        private static readonly Cache<string, ValueObjectHolder> _valueObjectCache = new Cache<string, ValueObjectHolder>();

        //public static IEnumerable<ValueObject> GetAllActive(string name)
        //{
        //    return _valueObjectCache[name];
        //}

        //public static IEnumerable<ValueObject> GetAllActive<T>()
        //{
        //    return _valueObjectCache[typeof (T).Name];
        //}

        public static ValueObject FindDefault(string listName)
        {
            return _valueObjectCache[listName].Default();
        }

        //public static void AddValueObjects(string key, IEnumerable<ValueObject> objects)
        //{
        //    _valueObjectCache.Store(key, objects);
        //}

        public static void AddValueObjects<T>(IEnumerable<ValueObject> objects)
        {
            var holder = new ValueObjectHolder(typeof (T).Name);
            holder.Values = objects;
            _valueObjectCache.Store(holder.GetKey(), holder);
        }

        public static void RemoveObject(string key)
        {
            _valueObjectCache.Remove(key);
        }

        public static ValueObjectHolder GetValueObjectHolder(string name)
        {
            return _valueObjectCache[name];
        }
    }

    public class ValueObjectHolder
    {
        private string _key;

        public ValueObjectHolder(string key)
        {
            _key = key;
        }



        public IEnumerable<ValueObject> Values
        {
            get; set;
        }

        public string GetKey()
        {
            return _key;
        }

        public ValueObject Default()
        {
            return Values.Where(x => x.IsDefault).FirstOrDefault() ?? Values.FirstOrDefault();
        }
    }

    public class ValueObjectInitializer : IValueObjectInitializer
    {
        private readonly ICompanyService _companyService;

        public ValueObjectInitializer(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public void Start()
        {
            var list = _companyService.ListAllCompanies().Select(x => new ValueObject(x.Id.ToString(), x.Name));

            ValueObjectRegistry.AddValueObjects<Company>(list);
        }
    }

    public interface IValueObjectInitializer : IStartable
    {
    }

    public interface IStartable
    {
        void Start();
    }
}