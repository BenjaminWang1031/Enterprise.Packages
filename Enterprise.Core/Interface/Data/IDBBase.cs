using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;

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
        /// Set the time in seconds to wait for the command to execute. The default is 600 seconds.
        /// </summary>
        int Timeout { get; set; }

        /// <summary>
        /// Error Message
        /// </summary>
        string ErrorMsg { get; set; }
    }
}
