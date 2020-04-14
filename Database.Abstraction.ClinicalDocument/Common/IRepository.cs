using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Database.Abstraction.ClinicalDocument.Common
{
    /// <summary>
    /// Generic Respository Interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Marks an entity as new
        /// </summary>
        /// <param name="entity">geneic entity</param>
        void Add(T entity);

        /// <summary>
        /// Marks an entity as modified
        /// </summary>
        /// <param name="entity">geneic entity</param>
        void Update(T entity);

        /// <summary>
        /// Marks an entity to be removed by id
        /// </summary>
        /// <param name="id">key value as id</param>
        void DeleteById(int id);

        /// <summary>
        /// Marks an entity to be removed
        /// </summary>
        /// <param name="entity">generic entity</param>
        void Delete(T entity);

        /// <summary>
        /// Marks an entity to be removed
        /// </summary>
        /// <param name="where">condition</param>
        void Delete(Expression<Func<T, bool>> where);

        /// <summary>
        /// Get an entity by int id
        /// </summary>
        /// <param name="id">key value as id</param>
        /// <returns></returns>
        T GetById(int id);

        /// <summary>
        /// Gets all entities of type T
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Gets entities using delegate
        /// </summary>
        /// <param name="where">condition</param>
        /// <returns></returns>
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);

        /// <summary>
        /// Find Consent using query 
        /// </summary>
        /// <param name="predicate">predicate</param>
        /// <returns></returns>
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// get consent and consentdetail
        /// </summary>
        /// <param name="includes">multiple entities</param>
        /// <returns></returns>
        IQueryable<T> GetIncluding(params Expression<Func<T, object>>[] includes);

    }
}
