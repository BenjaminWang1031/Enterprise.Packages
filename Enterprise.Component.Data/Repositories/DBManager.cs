using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;
using NHibernate.Transform;
using Enterprise.Core.Interface.Data;
using Enterprise.Component.Nhiberate.Base;

namespace Enterprise.Component.Nhiberate
{
    public class DBManager : DisposableObject,IDBManager
    {
        #region Private Fields

        [ThreadStatic]
        private bool committed = true;
        [ThreadStatic]
        private readonly SessionFactory databaseSessionFactory;
        [ThreadStatic]
        private readonly ISession session = null;
        [ThreadStatic]
        private readonly List<object> newObjects = new List<object>();
        [ThreadStatic]
        private readonly List<object> modifiedObjects = new List<object>();
        [ThreadStatic]
        private readonly Dictionary<string, string> modifiedObjectsOldValue = new Dictionary<string, string>();
        [ThreadStatic]
        private readonly List<object> deletedObjects = new List<object>();
        [ThreadStatic]
        private string mErrorMsg;

        #endregion

        #region override
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ISession dbSession = session;
                if (dbSession != null && dbSession.IsOpen)
                {
                    dbSession.Close();
                }
                dbSession.Dispose();
            }
        }
        #endregion
        
        #region Ctor
        public DBManager()
            : this("") { }

        public DBManager(string configFileName)
        {
            databaseSessionFactory = new SessionFactory(configFileName);
            session = databaseSessionFactory.Session;
            
        }
        #endregion

        #region Add
        
        /// <summary>
        /// Add single entity
        /// </summary>
        /// <param name="EntityObj"></param>
        public void Add(IEntityRoot EntityObj)
        {
            if (EntityObj != null)
                newObjects.Add(EntityObj);
            committed = false;
        }

        /// <summary>
        /// Add entity list
        /// </summary>
        /// <param name="EntityList"></param>
        public void Add(List<IEntityRoot> EntityList)
        {
            foreach (var EntityObj in EntityList)
            {
                if (EntityObj != null)
                    newObjects.Add(EntityObj);
            }
            committed = false;
        }

        #endregion

        #region Update

        /// <summary>
        /// Update single entity
        /// </summary>
        /// <param name="EntityObj"></param>
        public void Update(IEntityRoot EntityObj)
        {
            if (EntityObj != null)
                modifiedObjects.Add(EntityObj);
            committed = false;
        }

        public void Update(List<IEntityRoot> EntityList)
        {
            if (EntityList != null && EntityList.Count > 0)
            { 
                foreach (var entity in EntityList)
                    modifiedObjects.Add(entity);
            }                
            committed = false;
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete single entity
        /// </summary>
        /// <param name="EntityObj"></param>
        public void Delete(IEntityRoot EntityObj)
        {
            if (EntityObj != null)
                deletedObjects.Add(EntityObj);
            committed = false;
        }

        /// <summary>
        /// Delete single entity by key value
        /// </summary>
        /// <typeparam name="TEntityRoot"></typeparam>
        /// <param name="key"></param>
        public void Delete<TEntityRoot>(object key) where TEntityRoot : class,new()
        {
            if (key != null)
            {
                var obj = session.Get<TEntityRoot>(key);
                if (obj != null)
                {
                    Delete((IEntityRoot)(Object)obj);
                }
            }
        }

        /// <summary>
        /// Delete entity list
        /// </summary>
        /// <param name="list"></param>
        public void Delete(List<IEntityRoot> list)
        {
            if (list != null && list.Count > 0)
            {
                foreach (var entity in list)
                {
                    this.Delete((IEntityRoot)entity);
                }
            }
        }

        #endregion

        #region Query single entity

        /// <summary>
        /// Get single entity by key value
        /// </summary>
        /// <typeparam name="TEntityRoot"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public TEntityRoot Get<TEntityRoot>(object key) where TEntityRoot : class,new()
        {
            if (key == null)
                return null;
            var obj = session.Get<TEntityRoot>(key);
            return obj;
        }

        /// <summary>
        /// Get a single entity with a specification
        /// </summary>
        /// <typeparam name="TEntityRoot"></typeparam>
        /// <param name="specification"></param>
        /// <returns></returns>
        public TEntityRoot Get<TEntityRoot>(ISpecification<TEntityRoot> specification) where TEntityRoot : class, new()
        {
            var list = session.Query<TEntityRoot>().Where(specification.SatisfiedBy()).ToList();
            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            return null;
        }

        #endregion

        #region Query entity list
        
        /// <summary>
        /// Get all entities in a list
        /// </summary>
        /// <typeparam name="TEntityRoot"></typeparam>
        /// <returns></returns>
        public List<TEntityRoot> GetAll<TEntityRoot>() where TEntityRoot : class,new()
        {
            return GetAll(new TrueSpecification<TEntityRoot>(), null, DBSortOrder.Unspecified).ToList();
        }

        public List<TEntityRoot> GetAll<TEntityRoot>(ISpecification<TEntityRoot> specification) where TEntityRoot : class, new()
        {
            return GetAll(specification, null, DBSortOrder.Unspecified);
        }

        public List<TEntityRoot> GetAll<TEntityRoot>(Expression<Func<TEntityRoot, object>> SortPredicate, DBSortOrder SortOrder) where TEntityRoot : class, new()
        {
            return GetAll(new TrueSpecification<TEntityRoot>(), SortPredicate, SortOrder);
        }

        public List<TEntityRoot> GetAll<TEntityRoot>(ISpecification<TEntityRoot> Specification, Expression<Func<TEntityRoot, object>> SortPredicate, DBSortOrder SortOrder) where TEntityRoot : class, new()
        {
            var query = this.session.Query<TEntityRoot>()
                .Where(Specification.SatisfiedBy());
            switch (SortOrder)
            {
                case DBSortOrder.Ascending:
                    if (SortPredicate != null)
                        return query.OrderBy(SortPredicate).AsEnumerable().ToList();
                    break;
                case DBSortOrder.Descending:
                    if (SortPredicate != null)
                        return query.OrderByDescending(SortPredicate).AsEnumerable().ToList();
                    break;
                default:
                    break;
            }

            return query.AsEnumerable().ToList();
        }

        #endregion

        #region Query with original sql string

        public DataTable GetDataTable(string ProcedureName, object[] Param)
        {
            var cmd = session.Connection.CreateCommand();
            cmd.CommandTimeout = 120000;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = ProcedureName;
            foreach (var o in Param)
            {
                cmd.Parameters.Add(o);
            }
            try
            {
                using (var reader = cmd.ExecuteReader())
                {

                    DataTable table = new DataTable();
                    if (reader != null && reader.FieldCount > 0)
                    {

                        for (int i = 0; i < reader.FieldCount; i++)
                            table.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
                        while (reader.Read())
                        {
                            DataRow row = table.NewRow();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[i] = reader[i];
                            }
                            table.Rows.Add(row);
                        }
                    }
                    return table;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
            }
        }

        /// <summary>
        /// Execute sql string
        /// </summary>
        /// <param name="SqlString">SQL</param>
        /// <returns>datatable</returns>
        public DataTable GetDataTable(string SqlString)
        {
            try
            {
                var cmd = session.Connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SqlString;
                var reader = cmd.ExecuteReader();
                var tab = new DataTable();
                tab.Load(reader);
                return tab;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Execute procedure to get dataset
        /// </summary>
        /// <param name="ProcedureName"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string ProcedureName, object[] Param)
        {
            try
            {
                using (var conn = session.Connection as SqlConnection)
                {
                    conn.Open();
                    var cmd = conn.CreateCommand();
                    cmd.CommandTimeout = 120000;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = ProcedureName;
                    foreach (var o in Param)
                    {
                        cmd.Parameters.Add(o);
                    }
                    var adapter = new SqlDataAdapter(cmd);
                    var dataSet = new System.Data.DataSet();
                    adapter.Fill(dataSet);
                    return dataSet;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }


        /// <summary>
        /// Execute sql string
        /// </summary>
        /// <param name="SqlString">SQL</param>
        /// <returns>datatable</returns>
        public DataSet GetDataSet(string SqlString)
        {
            try
            {
                using (var conn = session.Connection as SqlConnection)
                {
                    conn.Open();
                    var adapter = new SqlDataAdapter(SqlString, conn);
                    var dataSet = new System.Data.DataSet();
                    adapter.Fill(dataSet);
                    return dataSet;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Execute sql

        public int ExecuteNonQuery(string SqlString)
        {
            try
            {
                var cmd = session.Connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SqlString;
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Paged query

        /// <summary>
        /// Paged data
        /// </summary>
        /// <typeparam name="TAggregateRoot"></typeparam>
        /// <param name="pageIndex">started from 1</param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public IEnumerable<TAggregateRoot> GetPaged<TAggregateRoot>(int PageIndex, int PageSize)
            where TAggregateRoot : class,new()
        {
            return this.GetPaged(PageIndex, PageSize, new TrueSpecification<TAggregateRoot>(), null, DBSortOrder.Unspecified);
        }

        /// <summary>
        /// Paged data
        /// </summary>
        /// <typeparam name="TAggregateRoot"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="specification"></param>
        /// <returns></returns>
        public IEnumerable<TAggregateRoot> GetPaged<TAggregateRoot>(int PageIndex, int PageSize, ISpecification<TAggregateRoot> Specification)
            where TAggregateRoot : class,new()
        {
            return this.GetPaged(PageIndex, PageSize, Specification, null, DBSortOrder.Unspecified);
        }

        /// <summary>
        /// Paged data
        /// </summary>
        /// <typeparam name="TAggregateRoot"></typeparam>
        /// <param name="pageIndex">started from 1</param>
        /// <param name="pageCount"></param>
        /// <param name="sortPredicate"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public IEnumerable<TAggregateRoot> GetPaged<TAggregateRoot>(int PageIndex, int PageSize, Expression<Func<TAggregateRoot, dynamic>> SortPredicate, DBSortOrder SortOrder)
            where TAggregateRoot : class,new()
        {
            return this.GetPaged(PageIndex, PageSize, new TrueSpecification<TAggregateRoot>(), SortPredicate, SortOrder);
        }

        /// <summary>
        /// Paged data
        /// </summary>
        /// <typeparam name="TAggregateRoot"></typeparam>
        /// <param name="pageIndex">started from 1</param>
        /// <param name="pageCount"></param>
        /// <param name="specification"></param>
        /// <param name="sortPredicate"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public IEnumerable<TAggregateRoot> GetPaged<TAggregateRoot>(int PageIndex, int PageSize, ISpecification<TAggregateRoot> Specification, Expression<Func<TAggregateRoot, dynamic>> SortPredicate, DBSortOrder SortOrder)
            where TAggregateRoot : class,new()
        {

            var query = session.Query<TAggregateRoot>();

            if (Specification != null)
            {
                query = query.Where(Specification.SatisfiedBy());
            }
            switch (SortOrder)
            {
                case DBSortOrder.Ascending:
                    if (SortPredicate != null)
                        query = query.OrderBy(SortPredicate);
                    break;
                case DBSortOrder.Descending:
                    if (SortPredicate != null)
                        query = query.OrderByDescending(SortPredicate);
                    break;
                default:
                    break;
            }
            return query.Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();
        }

        #endregion

        #region Commit

        /// <summary>
        /// Commit
        /// </summary>
        /// <returns></returns>
        public bool Commit()
        {
            ITransaction trans = session.BeginTransaction();

            try
            {
                foreach (object obj in newObjects)
                    session.Save(obj);
                foreach (object obj in modifiedObjects)
                    session.Merge(obj);
                foreach (object obj in deletedObjects)
                    session.Delete(obj);
                trans.Commit();

                newObjects.Clear();
                deletedObjects.Clear();
                modifiedObjects.Clear();

                committed = true;
                return true;
            }
            catch (Exception ex)
            {
                if (trans.IsActive)
                    trans.Rollback();
                this.session.Clear();
                throw new Exception(ex.InnerException == null ? ex.Message : (ex.InnerException).Message);
            }
            finally
            {
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }

        #endregion

        #region Public field

        public ISession Session { get { return session; } }

        /// <summary>
        /// Error message if any error occured
        /// </summary>
        public string ErrorMsg
        {
            get
            {
                return this.mErrorMsg;
            }
            set
            {
                this.mErrorMsg = value;
            }
        }
        #endregion
    }
}
