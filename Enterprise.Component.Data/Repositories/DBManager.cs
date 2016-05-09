﻿using System;
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
using Enterprise.Core.Interface.Data.Specification;
using Enterprise.Component.Nhiberate.Base;

namespace Enterprise.Component.Nhiberate
{
    /// <summary>
    /// @Desc: NHiberate implementation of the database objects manager
    /// @Author: Benjamin Wang
    /// @Date: 02/2016
    /// </summary>
    public class DBManager : DisposableObject, IDBManager
    {
        #region Private Fields
        [ThreadStatic]
        private bool mCommitted = true;
        [ThreadStatic]
        private readonly SessionFactory mDatabaseSessionFactory;
        [ThreadStatic]
        private readonly ISession mSession = null;
        [ThreadStatic]
        private readonly List<object> mNewObjects = new List<object>();
        [ThreadStatic]
        private readonly List<object> mUpdatedObjects = new List<object>();
        [ThreadStatic]
        private readonly List<object> mDeletedObjects = new List<object>();
        [ThreadStatic]
        private string mErrorMsg;
        [ThreadStatic]
        private int mConnectionTimeout = 60;
        [ThreadStatic]
        private int mCommandTimeout = 1800;
        #endregion

        #region override
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (mSession != null && mSession.IsOpen)
                {
                    mSession.Close();
                }
                mSession.Dispose();
            }
        }
        #endregion

        #region Ctor
        public DBManager()
            : this("") { }

        public DBManager(string ConnectionString)
        {
            mDatabaseSessionFactory = new SessionFactory(ConnectionString);
            mSession = mDatabaseSessionFactory.Session;
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
                mNewObjects.Add(EntityObj);
            mCommitted = false;
        }

        /// <summary>
        /// Add entity list
        /// </summary>
        /// <param name="EntityList"></param>
        public void Add(List<IEntityRoot> EntityList)
        {
            foreach (var EntityObj in EntityList)
                Add(EntityObj);
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
                mUpdatedObjects.Add(EntityObj);
            mCommitted = false;
        }

        public void Update(List<IEntityRoot> EntityList)
        {
            if (EntityList != null && EntityList.Count > 0)
            {
                foreach (var entity in EntityList)
                    Update(entity);
            }
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
                mDeletedObjects.Add(EntityObj);
            mCommitted = false;
        }

        /// <summary>
        /// Delete single entity by key value
        /// </summary>
        /// <typeparam name="TEntityRoot"></typeparam>
        /// <param name="key"></param>
        public void Delete<TEntityRoot>(object Key) where TEntityRoot : class,new()
        {
            if (Key != null)
            {
                var obj = this.GetEntity<TEntityRoot>(Key);
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
        public void Delete(List<IEntityRoot> EntityList)
        {
            if (EntityList != null && EntityList.Count > 0)
            {
                foreach (var entity in EntityList)
                    this.Delete((IEntityRoot)entity);
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
        public TEntityRoot GetEntity<TEntityRoot>(object Key) where TEntityRoot : class,new()
        {
            if (Key == null)
                return null;
            return mSession.Get<TEntityRoot>(Key);
        }

        /// <summary>
        /// Get a single entity with a specification
        /// </summary>
        /// <typeparam name="TEntityRoot"></typeparam>
        /// <param name="specification"></param>
        /// <returns></returns>
        public TEntityRoot GetEntity<TEntityRoot>(ISpecification<TEntityRoot> specification) where TEntityRoot : class, new()
        {
            var list = mSession.Query<TEntityRoot>().Where(specification.SatisfiedBy()).ToList();
            return (list != null && list.Count > 0) ? list[0] : null;
        }

        /// <summary>
        /// Get a single entity with Expression
        /// </summary>
        /// <typeparam name="TEntityRoot"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public TEntityRoot GetEntity<TEntityRoot>(Expression<Func<TEntityRoot, bool>> where) where TEntityRoot : class, new()
        {
            var list = mSession.Query<TEntityRoot>().Where(where).ToList();
            return (list != null && list.Count > 0) ? list[0] : null;
        }

        #endregion

        #region Query entity list

        public List<TEntityRoot> GetEntityList<TEntityRoot>(Expression<Func<TEntityRoot, bool>> where) where TEntityRoot : class,new()
        {
            return mSession.Query<TEntityRoot>().Where(where).ToList();
        }

        /// <summary>
        /// Get all entities in a list
        /// </summary>
        /// <typeparam name="TEntityRoot"></typeparam>
        /// <returns></returns>
        public List<TEntityRoot> GetEntityList<TEntityRoot>() where TEntityRoot : class,new()
        {
            return GetEntityList(new TrueSpecification<TEntityRoot>(), null, DBSortOrder.Unspecified).ToList();
        }

        public List<TEntityRoot> GetEntityList<TEntityRoot>(ISpecification<TEntityRoot> specification) where TEntityRoot : class, new()
        {
            return GetEntityList(specification, null, DBSortOrder.Unspecified);
        }

        public List<TEntityRoot> GetEntityList<TEntityRoot>(Expression<Func<TEntityRoot, object>> SortPredicate, DBSortOrder SortOrder) where TEntityRoot : class, new()
        {
            return GetEntityList(new TrueSpecification<TEntityRoot>(), SortPredicate, SortOrder);
        }

        public List<TEntityRoot> GetEntityList<TEntityRoot>(ISpecification<TEntityRoot> Specification, Expression<Func<TEntityRoot, object>> SortPredicate, DBSortOrder SortOrder) where TEntityRoot : class, new()
        {
            var query = this.mSession.Query<TEntityRoot>()
                .Where(Specification.SatisfiedBy()).Timeout(this.mCommandTimeout);
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

        /// <summary>
        /// Execute SP to return a datatable
        /// </summary>
        /// <param name="ProcedureName"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string ProcedureName, object[] Param)
        {
            var cmd = mSession.Connection.CreateCommand();
            cmd.CommandTimeout = this.mCommandTimeout;
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
            catch (Exception ex)
            {
                this.mErrorMsg = ex.Message;
                throw ex;
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
                var cmd = mSession.Connection.CreateCommand();
                cmd.CommandTimeout = this.mCommandTimeout;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SqlString;
                var reader = cmd.ExecuteReader();
                var tab = new DataTable();
                tab.Load(reader);
                return tab;
            }
            catch (Exception ex)
            {
                this.mErrorMsg = ex.Message;
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
                using (var conn = mSession.Connection as SqlConnection)
                {
                    conn.Open();
                    var cmd = conn.CreateCommand();
                    cmd.CommandTimeout = this.mCommandTimeout;
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
                this.mErrorMsg = exception.Message;
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
                using (var conn = mSession.Connection as SqlConnection)
                {
                    conn.Open();
                    var adapter = new SqlDataAdapter(SqlString, conn);
                    adapter.SelectCommand.CommandTimeout = this.mCommandTimeout;
                    var dataSet = new System.Data.DataSet();
                    adapter.Fill(dataSet);
                    return dataSet;
                }
            }
            catch (Exception ex)
            {
                this.mErrorMsg = ex.Message;
                throw ex;
            }
        }

        #endregion

        #region Execute sql

        public int ExecuteNonQuery(string SqlString)
        {
            try
            {
                var cmd = mSession.Connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SqlString;
                cmd.CommandTimeout = this.mCommandTimeout;
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                this.mErrorMsg = ex.Message;
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
        public IEnumerable<TAggregateRoot> GetPagedList<TAggregateRoot>(int PageIndex, int PageSize)
            where TAggregateRoot : class,new()
        {
            return this.GetPagedList(PageIndex, PageSize, new TrueSpecification<TAggregateRoot>(), null, DBSortOrder.Unspecified);
        }

        /// <summary>
        /// Paged data
        /// </summary>
        /// <typeparam name="TAggregateRoot"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="specification"></param>
        /// <returns></returns>
        public IEnumerable<TAggregateRoot> GetPagedList<TAggregateRoot>(int PageIndex, int PageSize, ISpecification<TAggregateRoot> Specification)
            where TAggregateRoot : class,new()
        {
            return this.GetPagedList(PageIndex, PageSize, Specification, null, DBSortOrder.Unspecified);
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
        public IEnumerable<TAggregateRoot> GetPagedList<TAggregateRoot>(int PageIndex, int PageSize, Expression<Func<TAggregateRoot, dynamic>> SortPredicate, DBSortOrder SortOrder)
            where TAggregateRoot : class,new()
        {
            return this.GetPagedList(PageIndex, PageSize, new TrueSpecification<TAggregateRoot>(), SortPredicate, SortOrder);
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
        public IEnumerable<TAggregateRoot> GetPagedList<TAggregateRoot>(int PageIndex, int PageSize, ISpecification<TAggregateRoot> Specification, Expression<Func<TAggregateRoot, dynamic>> SortPredicate, DBSortOrder SortOrder)
            where TAggregateRoot : class,new()
        {

            var query = mSession.Query<TAggregateRoot>().Timeout(this.mCommandTimeout);

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
            if (mCommitted)
            {
                this.mErrorMsg = "Current changes were already committed!";
                return false;
            }
            using (var trans = mSession.BeginTransaction())
            {
                try
                {
                    //apply changes
                    foreach (object obj in mNewObjects)
                        mSession.Save(obj);
                    foreach (object obj in mUpdatedObjects)
                        mSession.Update(obj); //mSession.Merge(obj); // use 'merge' can do a 'save'/'update' action. 
                    foreach (object obj in mDeletedObjects)
                        mSession.Delete(obj);
                    //commit to database
                    trans.Commit();
                    //clear lists
                    mNewObjects.Clear();
                    mUpdatedObjects.Clear();
                    mDeletedObjects.Clear();
                    //set flag
                    mCommitted = true;

                    return true;
                }
                catch (Exception ex)
                {
                    if (trans.IsActive)
                        trans.Rollback();
                    this.mSession.Clear();
                    this.mErrorMsg = ex.Message;
                    throw new Exception(ex.InnerException == null ? ex.Message : (ex.InnerException).Message);
                }
            }
        }

        #endregion

        #region Public field
        //could be used to include sql strings inside one transaction
        //public ISession Session { get { return mSession; } }

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

        /// <summary>
        /// set the ConnectionTimeout
        /// </summary>
        public int ConnectionTimeout
        {
            get
            {
                return this.mConnectionTimeout;
            }
            set
            {
                this.mConnectionTimeout = value;
            }
        }

        /// <summary>
        /// set the CommandTimeout
        /// </summary>
        public int CommandTimeout
        {
            get
            {
                return this.mCommandTimeout;
            }
            set
            {
                this.mCommandTimeout = value;
            }
        }

        #endregion
    }
}
