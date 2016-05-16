using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enterprise.Core.Interface.Common;

namespace Enterprise.Core.Interface.Log
{
    /// <summary>
    /// Log base interface
    /// @Author: Benjamin Wang
    /// @Dec: Base interface for logger
    /// @Date:02/2016
    /// </summary>
    public interface ILogHelper
    {
         /// <summary>
         /// Pack all the logs in log folder
         /// </summary>
         /// <param name="ZipHelper">Zip Helper instance</param>
         /// <param name="LogFolder">Log's whole folder</param>
         /// <returns></returns>
        string PackLogs(IZipHelper ZipHelper, string LogFolder, string AppName = "");
    }
}
