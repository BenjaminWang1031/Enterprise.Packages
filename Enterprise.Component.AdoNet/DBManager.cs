using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using Enterprise.Core.Interface.Data;

namespace Enterprise.Component.AdoNet
{
    public class DBManager : DisposableObject, IDBBase
    {
        #region Private Members

        private string mConnectString;
        private string mErrorMsg;
        /// <summary>
        /// Default to 600 seconds
        /// </summary>
        private int mTimeout = 600;

        #endregion

        #region Ctr

        public DBManager(string ConnectString = "")
        {
            this.mConnectString = string.IsNullOrEmpty(ConnectString) ? GetConnectstring() : ConnectString;
        }

        #endregion

        /// <summary>
        /// Execute stored proc to get a datatable
        /// </summary>
        /// <param name="ProcedureName"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string ProcedureName, object[] Param)
        {
            try
            {
                using (var conn = new SqlConnection(mConnectString))
                {
                    conn.Open();
                    var cmd = conn.CreateCommand();
                    cmd.CommandTimeout = this.mTimeout;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = ProcedureName;
                    foreach (var o in Param)
                    {
                        cmd.Parameters.Add(o);
                    }
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
            }
            catch (Exception exception)
            {
                this.mErrorMsg = exception.Message;
                throw exception;
            }
        }

        public DataSet GetDataSet(string ProcedureName, object[] Param)
        {
            try
            {
                using (var conn = new SqlConnection(mConnectString))
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
            catch (Exception ex)
            {
                this.mErrorMsg = ex.Message;
                throw ex;
            }
        }

        public DataTable GetDataTable(string SqlString)
        {
            try
            {
                using (var conn = new SqlConnection(mConnectString))
                {
                    conn.Open();
                    var cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SqlString;
                    var reader = cmd.ExecuteReader();
                    var tab = new DataTable();
                    tab.Load(reader);
                    return tab;
                }
            }
            catch (Exception ex)
            {
                this.mErrorMsg = ex.Message;
                throw ex;
            }
        }

        public DataSet GetDataSet(string SqlString)
        {
            try
            {
                using (var conn = new SqlConnection(mConnectString))
                {
                    conn.Open();
                    var adapter = new SqlDataAdapter(SqlString, conn);
                    var dataSet = new DataSet();
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

        public int ExecuteNonQuery(string SqlString)
        {
            try
            {
                using (var conn = new SqlConnection(mConnectString))
                {
                    conn.Open();
                    var cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SqlString;
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                this.mErrorMsg = ex.Message;
                throw ex;
            }
        }

        protected override void Dispose(bool disposing)
        {
            //if (disposing)
            //{
            //    base.Dispose();
            //}
        }

        public int Timeout
        {
            get { return this.mTimeout; }
            set { this.mTimeout = value; }
        }
        public string ErrorMsg
        {
            get { return this.mErrorMsg; }
            set { this.mErrorMsg = value; }
        }

        private string GetConnectstring()
        {
            return ConfigurationManager.ConnectionStrings["Connection_String"].ConnectionString;
        }
    }
}
