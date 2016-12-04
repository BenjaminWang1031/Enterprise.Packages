using System;
using System.Data;
using Enterprise.Core.Interface.Log;

namespace Enterprise.Core.Interface.Data
{
    /// <summary>
    /// @Dec:Base database object interface
    /// @Author:Benjamin Wang
    /// @Date:02/2016
    /// </summary>
    public interface IDBBase:IDisposable 
    {
        /// <summary>
        /// Execute Procedure to get a datatable
        /// </summary>
        /// <param name="ProcedureName"></param>
        /// <param name="Param"></param>
        /// <returns>datatable</returns>
        DataTable GetDataTable(string ProcedureName, object[] Param);

        /// <summary>
        /// Execute Procedure to get a dataset
        /// </summary>
        /// <param name="ProcedureName"></param>
        /// <param name="Param"></param>
        /// <returns>Dataset</returns>
        DataSet GetDataSet(string ProcedureName, object[] Param);

        /// <summary>
        /// Execute SQL string to get a datatable
        /// </summary>
        /// <param name="SqlString"></param>
        /// <returns>datatable</returns>
        DataTable GetDataTable(string SqlString);

        /// <summary>
        /// Execute SQL string to get a dataset
        /// </summary>
        /// <param name="SqlString"></param>
        /// <returns>Dataset</returns>
        DataSet GetDataSet(string SqlString);

        /// <summary>
        /// Execute sql string
        /// </summary>
        /// <param name="SqlString"></param>
        /// <returns></returns>
        int ExecuteNonQuery(string SqlString);

        /// <summary>
        /// Set the time in seconds for a connection. The default is 60 seconds.(1 min)
        /// </summary>
        int ConnectionTimeout { get; set; }

        /// <summary>
        /// Set the time in seconds for a command to be executed. The default is 1800 seconds(30 mins).
        /// To set it =0 if no timeout is required
        /// </summary>
        int CommandTimeout { get; set; }

        /// <summary>
        /// Error Message
        /// </summary>
        string ErrorMsg { get; set; }

        /// <summary>
        /// Log helper
        /// For exception/performance
        /// </summary>
        ILogHelper DBLogHelper { get; set; }
    }
}
