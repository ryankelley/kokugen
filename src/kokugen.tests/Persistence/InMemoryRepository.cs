using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using FubuCore.Reflection;
using FubuCore.Util;
using Kokugen.Core.Domain;
using Kokugen.Core.Persistence;
using NHibernate;
using NHibernate.Criterion;


namespace Kokugen.Tests.Persistence
{
    /// <summary>
    /// Code copied from fluent nhibenate so that it will work with the 
    /// altered IRepository interface for our framework
    /// </summary>
    /// <remarks>
    /// Attribution: Fluent NHibernate
    /// Reference: http://fluentnhibernate.org/ 
    /// </remarks>
    public class InMemoryRepository<ENTITY>
        where ENTITY : Entity
    {
        private readonly Cache<Type, object> _types = new Cache<Type, object>();

        public InMemoryRepository()
        {
            _types =
               new Cache<Type, object>(
                   type => Activator.CreateInstance(
                               typeof(List<>).MakeGenericType(new[] { type })));
        }

        private IList<ENTITY> listFor<ENTITY>()
        {
            return (IList<ENTITY>)_types[typeof(ENTITY)];
        }



        #region IRepository Members

        public void Save(ENTITY entity)
        {
            listFor<ENTITY>().Add(entity);
        }

        public ENTITY Load(Guid id)
        {
            return listFor<ENTITY>()
                .FirstOrDefault(x => x.Id == id);
        }

        public virtual ENTITY FindBy<ENTITY, TU>(Expression<Func<ENTITY, TU>> expression, TU search) where ENTITY : class
        {
            var accessor = ReflectionHelper.GetAccessor(expression);
            Func<ENTITY, bool> predicate = delegate(ENTITY t)
            {
                var local = (TU)accessor.GetValue(t);
                return search.Equals(local);
            };
            return listFor<ENTITY>().FirstOrDefault(predicate);
        }

        public ENTITY Get(Guid id)
        {
            return listFor<ENTITY>()
               .FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<ENTITY> Query()
        {
            return listFor<ENTITY>().AsQueryable();
        }

        public IQueryable<ENTITY> Query(IDomainQuery<ENTITY> whereQuery)
        {
            return listFor<ENTITY>().Select(item => item).Where(
                    whereQuery.Expression.Compile()).AsQueryable();
        }

        public IQueryable<ENTITY> Query(Expression<Func<ENTITY, bool>> where)
        {
            return listFor<ENTITY>().Select(item => item).Where(
                    where.Compile()).AsQueryable();
        }

        public void Delete(ENTITY entity)
        {
            listFor<ENTITY>().Remove(entity);
        }

        public void DeleteAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ENTITY> FindAll(params ICriterion[] criteria)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// Execute the specified stored procedure with the given parameters and then converts
        /// the results using the supplied delegate.
        /// </summary>
        /// <typeparam name="T2">The collection type to return.</typeparam>
        /// <param name="converter">The delegate which converts the raw results.</param>
        /// <param name="sp_name">The name of the stored procedure.</param>
        /// <param name="parameters">Parameters for the stored procedure.</param>
        /// <returns></returns>
        public IEnumerable<T2> ExecuteStoredProcedure<T2>(Converter<SafeDataReader, T2> converter, string sp_name,
                                                          params Parameter[] parameters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T2> ExecuteStoredProcedure2<T2>(Converter<IDataReader, T2> converter, string sp_name,
                                                           params Parameter[] parameters)
        {
            throw new NotImplementedException();
        }



        public IQuery CreateQuery(string hqlQuery)
        {
            throw new NotImplementedException();
        }

        public ISQLQuery CreateSQLQuery(string sqlQuery)
        {
            throw new NotImplementedException();
        }

        #endregion

        public static void CreateDbDataParameters(IDbCommand command, Parameter[] parameters)
        {
            foreach (Parameter parameter in parameters)
            {
                IDbDataParameter sp_arg = command.CreateParameter();
                sp_arg.ParameterName = parameter.Name;
                sp_arg.Value = parameter.Value;
                command.Parameters.Add(sp_arg);
            }
        }



        public void Evict(ENTITY entity)
        {
            throw new NotImplementedException();
        }
    }

    public class InMemoryRepository : InMemoryRepository<Entity>, IRepository<Entity>
    {

    }

}