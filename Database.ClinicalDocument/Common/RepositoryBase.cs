using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using Database.Abstraction.ClinicalDocument.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Database.ClinicalDocument.Common
{
    /// <summary>
    /// Repository Base
    /// </summary>
    /// <typeparam name="T"></typeparam>

    [ExcludeFromCodeCoverage]
    public abstract class RepositoryBase<T> : IRepository<T>
        where T : class
    {

        /// <summary>
        /// default constructor
        /// </summary>
        protected RepositoryBase()
        {

        }


        /// <summary>
        ///Initialize db context 
        /// </summary>
        /// <param name="dbFactory">database factory</param>
        protected RepositoryBase(DbContext context)
        {

            dataContext = context;
            this.dbSet = this.dataContext.Set<T>();
        }

        private DbContext dataContext;

        private readonly DbSet<T> dbSet;

        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Add(T entity)
        {
            this.dbSet.Add(entity);
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">generic entity</param>
        public virtual void Update(T entity)
        {
            this.dbSet.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }


        /// <summary>
        /// delete entity
        /// </summary>
        /// <param name="entity">generic entity</param>
        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }


        /// <summary>
        /// delete entity by key
        /// </summary>
        /// <param name="id">id</param>
        public void DeleteById(int id)
        {
            var entity = GetById(id);
            if (entity == null)
            {
                dbSet.Remove(entity);
            }
        }

        /// <summary>
        /// delete entity by expression
        /// </summary>
        /// <param name="where"></param>
        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
            {
                dbSet.Remove(obj);
            }
        }

        /// <summary>
        /// get entity by key
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public virtual T GetById(int id)
        {
            return dbSet.Find(id);
        }

        /// <summary>
        /// get all data from data set
        /// </summary>
        /// <returns>list of records</returns>
        public virtual IEnumerable<T> GetAll()
        {
            IEnumerable<T> returnvalues;
            returnvalues = this.dbSet.AsEnumerable<T>();
            return returnvalues;

        }

        /// <summary>
        /// get many records from dataset
        /// </summary>
        /// <param name="where">condition</param>
        /// <returns>list of records</returns>
        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> returnvalues;
            returnvalues = this.dbSet.AsEnumerable<T>();
            return returnvalues;

        }

        /// <summary>
        /// get single record from dataset
        /// </summary>
        /// <param name="where">condition</param>
        /// <returns>single record</returns>
        public T Get(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).FirstOrDefault<T>();
        }

        /// <summary>
        /// Find result with condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> enumerable = dataContext.Set<T>().Where(predicate);

            EnumerableQuery<T> queryable = new EnumerableQuery<T>(enumerable);
            return queryable;
        }

        /// <summary>
        /// include records from child table
        /// </summary>
        /// <param name="includes">multiple entities</param>
        /// <returns></returns>
        public IQueryable<T> GetIncluding(params Expression<Func<T, object>>[] includes)
        {
            IIncludableQueryable<T, object> query = null;

            if (includes.Length > 0)
            {
                query = dbSet.Include(includes[0]);
            }
            for (int queryIndex = 1; queryIndex < includes.Length; ++queryIndex)
            {
                query = query.Include(includes[queryIndex]);
            }

            return query == null ? dbSet : (IQueryable<T>)query;

        }

    }
}
