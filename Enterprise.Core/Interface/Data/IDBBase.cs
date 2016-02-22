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
    public interface IDBBase:IDisposable 
    {
        /// <summary>
        /// Execute Procedure
        /// </summary>
        /// <param name="ProcedureName"></param>
        /// <param name="Param"></param>
        /// <returns>datatable</returns>
        DataTable GetDataTable(string ProcedureName, object[] Param);
        DataSet GetDataSet(string ProcedureName, object[] Param);

        /// <summary>
        /// Execute SQL string
        /// </summary>
        /// <param name="SqlString"></param>
        /// <returns>datatable</returns>
        DataTable GetDataTable(string SqlString);
        DataSet GetDataSet(string SqlString);

        /// <summary>
        /// Execute sql string
        /// </summary>
        /// <param name="SqlString"></param>
        /// <returns></returns>
        int ExecuteNonQuery(string SqlString);

        /// <summary>
        /// Error Message
        /// </summary>
        string ErrorMsg { get; set; }
    }
}
