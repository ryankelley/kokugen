using System;
using System.Collections.Generic;
using System.Linq;
using FubuCore.Util;
using Kokugen.Core.Domain;
using Kokugen.Core.Services;

namespace Kokugen.Core
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

        public static void AddValueObjects(string key, IEnumerable<ValueObject> objects)
        {
            var holder = new ValueObjectHolder(key);
            holder.Values = objects;
            _valueObjectCache.Fill(holder.GetKey(), holder);
        }

        public static void AddValueObject<T>(ValueObject valueObject)
        {
            var holder = _valueObjectCache.Find(x => x.GetKey() == typeof (T).Name);

            holder.AddValue(valueObject);
        }

        public static void RemoveValueObject<T>(ValueObject valueObject)
        {
            var holder = _valueObjectCache.Find(x => x.GetKey() == typeof(T).Name);

            holder.RemoveValue(valueObject);
        }

        public static void AddValueObjects<T>(IEnumerable<ValueObject> objects)
        {
            var holder = new ValueObjectHolder(typeof (T).Name);
            holder.Values = objects;
            _valueObjectCache.Fill(holder.GetKey(), holder);
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

        private readonly IList<ValueObject> _values = new List<ValueObject>();


        public IEnumerable<ValueObject> Values
        {
            get { return _values; }
            set { _values.AddRange(value);}
        }

        public string GetKey()
        {
            return _key;
        }

        public ValueObject Default()
        {
            return Values.Where(x => x.IsDefault).FirstOrDefault() ?? Values.FirstOrDefault();
        }

        public void AddValue(ValueObject valueObject)
        {
            if(_values.Contains(valueObject))
                RemoveValue(valueObject);
            
            _values.Add(valueObject);
        }

        public void RemoveValue( ValueObject valueObject)
        {
            _values.Remove(valueObject);
        }

    }

    public class ValueObjectInitializer : IValueObjectInitializer
    {
        private readonly ICompanyService _companyService;
        private readonly ITaskCategoryService _taskCategoryService;
        private readonly IProjectService _projectService;

        public ValueObjectInitializer(ICompanyService companyService, ITaskCategoryService taskCategoryService, IProjectService projectService)
        {
            _companyService = companyService;
            _taskCategoryService = taskCategoryService;
            _projectService = projectService;
        }

        public void Start()
        {
            var list = _companyService.ListAllCompanies().Select(x => new ValueObject(x.Id.ToString(), x.Name));
            var taskList = _taskCategoryService.GetAllCategories().Select(x => new ValueObject(x.Id.ToString(), x.Name));
            var projectList = _projectService.ListProjects().Select(x => new ValueObject(x.Id.ToString(), x.Name));

            ValueObjectRegistry.AddValueObjects<Company>(list);
            ValueObjectRegistry.AddValueObjects<TaskCategory>(taskList);
            ValueObjectRegistry.AddValueObjects<Project>(projectList);
        }
    }

    public interface IValueObjectInitializer : IStartable
    {
    }
}