using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace Enterprise.Core.Interface.Export
{
    public interface IExportToExcelHelper:IExportHelper
    {
        string ExportToExcel(string SheetName, string FileName, DataTable  DataSource);

        DataTable ReadExcelData(string Fullpath);
    }
}
