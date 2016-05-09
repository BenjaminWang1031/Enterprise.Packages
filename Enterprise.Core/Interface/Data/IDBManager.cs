using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using Enterprise.Core.Interface.Data;

namespace Enterprise.Core.Interface.Data
{
    /// <summary>
    /// @Dec: Base OR Mapping interface
    /// @Author:Benjamin Wang
    /// @Date:02/2016
    /// </summary>
    public interface IDBManager : IDBBase
    {
        #region Add
        
        /// <summary>
        /// Add a single new entity
        /// </summary>
        /// <param name="EntityObj"></param>
        void Add(IEntityRoot EntityObj);

        /// <summary>
        /// Add a list of new entities
        /// </summary>
        /// <param name="EntityList"></param>
        void Add(List<IEntityRoot> EntityList);

        #endregion

        #region Update
           
        /// <summary>
        /// Update a single entity
        /// </summary>
        /// <param name="EntityObj"></param>
        void Update(IEntityRoot EntityObj);

        /// <summary>
        /// Update a list of entities
        /// </summary>
        /// <param name="EntityList"></param>
        void Update(List<IEntityRoot> EntityList);

        #endregion

        #region Delete
             
        /// <summary>
        /// Delete a single entity by entity
        /// </summary>
        /// <param name="EntityObj"></param>
        void Delete(IEntityRoot EntityObj);

        /// <summary>
        /// Delete a single entity by its key value
        /// TODO: Delete by Key function...Composite Primary Key
        /// </summary>
        /// <typeparam name="TEntityRoot"></typeparam>
        /// <param name="key"></param>
        void Delete<TEntityRoot>(object key) where TEntityRoot : class,new();   

        /// <summary>
        /// Delete a list of entities
        /// </summary>
        /// <param name="list"></param>
        void Delete(List<IEntityRoot> list);

        #endregion

        #region Query
       
        /// <summary>
        /// Query a single entity by its key
        /// </summary>
        /// <typeparam name="EntityRoot"></typeparam>
        /// <param name="specification"></param>
        /// <returns></returns>
        Entity GetEntity<Entity>(object key) where Entity : class,new();

        /// <summary>
        /// Query a single entity by specific criteria
        /// </summary>
        /// <typeparam name="TEntityRoot"></typeparam>
        /// <param name="specification"></param>
        /// <returns></returns>
        Entity GetEntity<Entity>(ISpecification<Entity> specification) where Entity : class,new();

        /// <summary>
        /// Query a single entity by Expression
        /// @:Newly added by Benjamin 05/06/2016
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        Entity GetEntity<Entity>(Expression<Func<Entity, bool>> where) where Entity : class, new();

        /// <summary>
        /// Query all the entities by entity type
        /// </summary>
        /// <typeparam name="TEntityRoot"></typeparam>
        /// <returns></returns>
        List<TEntityRoot> GetEntityList<TEntityRoot>() where TEntityRoot : class,new();

        /// <summary>
        /// Query a list of entities by specific criteria
        /// </summary>
        /// <typeparam name="TEntityRoot"></typeparam>
        /// <param name="specification"></param>
        /// <returns></returns>
        List<TEntityRoot> GetEntityList<TEntityRoot>(ISpecification<TEntityRoot> specification) where TEntityRoot : class,new();

        /// <summary>
        /// Query a list of entities by Expression
        /// @:Newly added by Benjamin 05/06/2016
        /// </summary>
        /// <typeparam name="TEntityRoot"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        List<TEntityRoot> GetEntityList<TEntityRoot>(Expression<Func<TEntityRoot, bool>> where) where TEntityRoot : class,new();

        /// <summary>
        ///  Query a list of entities with order by clause
        /// </summary>
        /// <typeparam name="TEntityRoot"></typeparam>
        /// <param name="SortPredicate"></param>
        /// <param name="SortOrder"></param>
        /// <returns></returns>
        List<TEntityRoot> GetEntityList<TEntityRoot>(Expression<Func<TEntityRoot, object>> SortPredicate, DBSortOrder SortOrder) where TEntityRoot : class,new();

        /// <summary>
        /// Query a list of entities by specific criteria with order by clause
        /// </summary>
        /// <typeparam name="TEntityRoot"></typeparam>
        /// <param name="specification"></param>
        /// <param name="sortPredicate"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        List<TEntityRoot> GetEntityList<TEntityRoot>(ISpecification<TEntityRoot> specification, Expression<Func<TEntityRoot, object>> sortPredicate, DBSortOrder sortOrder) where TEntityRoot : class,new();

        /// <summary>
        /// Query data with returning paged data
        /// </summary>
        /// <typeparam name="TAggregateRoot"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        IEnumerable<TAggregateRoot> GetPagedList<TAggregateRoot>(int PageIndex, int PageSize)
            where TAggregateRoot : class,new();

        /// <summary>
        /// Query data with returning paged data
        /// </summary>
        /// <typeparam name="TAggregateRoot"></typeparam>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="Specification"></param>
        /// <returns></returns>
        IEnumerable<TAggregateRoot> GetPagedList<TAggregateRoot>(int PageIndex, int PageSize, ISpecification<TAggregateRoot> Specification)
            where TAggregateRoot : class,new();

        /// <summary>
        /// Query data with returning paged data
        /// </summary>
        /// <typeparam name="TAggregateRoot"></typeparam>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="SortPredicate"></param>
        /// <param name="SortOrder"></param>
        /// <returns></returns>
        IEnumerable<TAggregateRoot> GetPagedList<TAggregateRoot>(int PageIndex, int PageSize, Expression<Func<TAggregateRoot, dynamic>> SortPredicate, DBSortOrder SortOrder)
            where TAggregateRoot : class,new();

        /// <summary>
        /// Query data with returning paged data
        /// </summary>
        /// <typeparam name="TAggregateRoot"></typeparam>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="Specification"></param>
        /// <param name="SortPredicate"></param>
        /// <param name="SortOrder"></param>
        /// <returns></returns>
        IEnumerable<TAggregateRoot> GetPagedList<TAggregateRoot>(int PageIndex, int PageSize, ISpecification<TAggregateRoot> Specification, Expression<Func<TAggregateRoot, dynamic>> SortPredicate, DBSortOrder SortOrder)
            where TAggregateRoot : class,new();

        #endregion

        /// <summary>
        /// Commit the changes of the entities
        /// Note: this doesn't contain the sql string part
        /// </summary>
        /// <returns></returns>
        bool Commit();

    }
}
