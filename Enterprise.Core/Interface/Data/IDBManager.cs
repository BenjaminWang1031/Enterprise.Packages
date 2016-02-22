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
    public interface IDBManager : IDBBase
    {
        /// <summary>
        /// Add function
        /// </summary>
        /// <param name="EntityObj"></param>
        void Add(IEntityRoot EntityObj);
        void Add(List<IEntityRoot> EntityList);

        /// <summary>
        /// Update function
        /// </summary>
        /// <param name="EntityObj"></param>
        void Update(IEntityRoot EntityObj);
        void Update(List<IEntityRoot> EntityList);

        /// <summary>
        /// Delete function
        /// </summary>
        /// <param name="EntityObj"></param>
        void Delete(IEntityRoot EntityObj);
        void Delete(List<IEntityRoot> list);
        void Delete<TEntityRoot>(object key) where TEntityRoot : class,new();        

        /// <summary>
        /// Query function
        /// </summary>
        /// <typeparam name="EntityRoot"></typeparam>
        /// <param name="specification"></param>
        /// <returns></returns>
        IEntityRoot Get<IEntityRoot>(object key) where IEntityRoot : class,new();
        List<TEntityRoot> GetAll<TEntityRoot>() where TEntityRoot : class,new();
        TEntityRoot Get<TEntityRoot>(ISpecification<TEntityRoot> specification) where TEntityRoot : class,new();
        List<TEntityRoot> GetAll<TEntityRoot>(ISpecification<TEntityRoot> specification) where TEntityRoot : class,new();
        List<TEntityRoot> GetAll<TEntityRoot>(Expression<Func<TEntityRoot, object>> SortPredicate, DBSortOrder SortOrder) where TEntityRoot : class,new();
        List<TEntityRoot> GetAll<TEntityRoot>(ISpecification<TEntityRoot> specification, Expression<Func<TEntityRoot, object>> sortPredicate, DBSortOrder sortOrder) where TEntityRoot : class,new();

        /// <summary>
        /// Paged query
        /// </summary>
        /// <typeparam name="TAggregateRoot"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        IEnumerable<TAggregateRoot> GetPaged<TAggregateRoot>(int PageIndex, int PageSize)
            where TAggregateRoot : class,new();
        IEnumerable<TAggregateRoot> GetPaged<TAggregateRoot>(int PageIndex, int PageSize, ISpecification<TAggregateRoot> Specification)
            where TAggregateRoot : class,new();
        IEnumerable<TAggregateRoot> GetPaged<TAggregateRoot>(int PageIndex, int PageSize, Expression<Func<TAggregateRoot, dynamic>> SortPredicate, DBSortOrder SortOrder)
            where TAggregateRoot : class,new();
        IEnumerable<TAggregateRoot> GetPaged<TAggregateRoot>(int PageIndex, int PageSize, ISpecification<TAggregateRoot> Specification, Expression<Func<TAggregateRoot, dynamic>> SortPredicate, DBSortOrder SortOrder)
            where TAggregateRoot : class,new();

        /// <summary>
        /// Commit
        /// </summary>
        /// <returns></returns>
        bool Commit();

    }
}
