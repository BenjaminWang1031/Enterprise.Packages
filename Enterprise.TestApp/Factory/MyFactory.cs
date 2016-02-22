using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Enterprise.Core.Interface.Log;
using Enterprise.Core.Interface.Export;
using Enterprise.Component.Log4Net;
using Enterprise.Component.Export;
using Enterprise.Component.Export.Excel;

namespace Enterprise.TestApp.Factory
{
    public class MyFactory
    {
        public static ILogExceptionHelper GetExceptionLogHelper()
        {
            return new LogExceptionHelper();
        }

        public static ILogUserHelper GetLogVisitorHelper()
        {
            return new LogUserHelper();
        }

        public static IExportToExcelHelper GetExportToExcelHelper()
        {
            return new NPOIExcelExpoter("ExcelExport");
        }
    }
}