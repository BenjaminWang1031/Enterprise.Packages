using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using Enterprise.Core.Interface.Export;


namespace Enterprise.Component.Export.Excel
{
    public class OpenXmlExcelExporter : IExportToExcelHelper
    {

        public string ExportToExcel(string SheetName, string FileName, DataTable DataSource)
        {
            throw new NotImplementedException();
        }

        public DataTable ReadExcelData(string Fullpath)
        {
            throw new NotImplementedException();
        }

        public string RootPathName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
