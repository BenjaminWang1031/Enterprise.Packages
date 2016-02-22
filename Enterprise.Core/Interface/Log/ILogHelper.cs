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
    /// </summary>
    public interface ILogHelper
    {
         void Init();

         string PackLogs(IZipHelper ZipHelper,string LogFolder);
    }
}
