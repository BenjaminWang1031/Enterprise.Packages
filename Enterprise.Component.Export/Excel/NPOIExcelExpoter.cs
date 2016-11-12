using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using System.IO;
using NPOI.HSSF.Util;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

using Enterprise.Core.Interface.Export;
using Enterprise.Component.Export.Common;

namespace Enterprise.Component.Export.Excel
{
    public class NPOIExcelExpoter : IExportToExcelHelper
    {
        private ExportHelper mExportHelper;

        public NPOIExcelExpoter(string RootFolderName)
        {
            this.RootPathName = RootFolderName;
            mExportHelper = new ExportHelper();
        }

        public string ExportToExcel(string SheetName, string FileName, DataTable DataSource)
        {
            if (DataSource.Rows.Count == 0)
                return string.Empty;

            if (DataSource.Columns.Contains("RowNum"))
            {
                DataSource.Columns.Remove("RowNum");
                DataSource.AcceptChanges();
            }

            //creat workbook
            var workbook = new XSSFWorkbook();
            var sheet = workbook.CreateSheet(SheetName);

            //creat header
            //first row / rowIndex=0
            IRow row;
            ICell cell;
            row = sheet.CreateRow(0);

            for (var i = 0; i <= DataSource.Columns.Count - 1; i++)
            {
                cell = row.CreateCell(i);
                cell.SetCellValue(DataSource.Columns[i].ColumnName);
                //'Set header Style
                var cellStyle = workbook.CreateCellStyle();
                cellStyle.FillPattern = FillPattern.SolidForeground;
                cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
                cell.CellStyle = cellStyle;
                //'Set column default width
                sheet.SetColumnWidth(i, 6000);
            }
            //'create records
            //'datarow  index starts from 0
            //'excelrow index starts from 1
            var rowIndex = 0;
            while (rowIndex < DataSource.Rows.Count)
            {
                row = sheet.CreateRow(rowIndex + 1);
                var colIndex = 0;
                while (colIndex < DataSource.Columns.Count)
                {
                    cell = row.CreateCell(colIndex);
                    cell.SetCellValue(string.Format("{0}", DataSource.Rows[rowIndex][colIndex]));
                    colIndex += 1;
                }
                rowIndex += 1;
            }

            if (!Directory.Exists(mExportHelper.DestFolder))
                Directory.CreateDirectory(mExportHelper.DestFolder);

            FileName = string.Format("{0}_{1}.xlsx", FileName, DateTime.Now.ToString("yyyyMMddHHmmss"));
            var path = string.Format("{0}/{1}", mExportHelper.DestFolder, FileName);
            var file = System.IO.File.Create(path);
            workbook.Write(file);
            file.Close();
            //Get prefix
            var urlAuthority = HttpContext.Current.Request.Url.Authority;
            //TODO: Get the app name and put into the url of the file
            var mPrefix = urlAuthority + HttpContext.Current.Request.ApplicationPath;
            if (!string.IsNullOrEmpty(AppName) && urlAuthority.ToLower().Contains(AppName.ToLower()))
                mPrefix = urlAuthority.Substring(0, urlAuthority.IndexOf(AppName) + 6) + HttpContext.Current.Request.ApplicationPath;
            return string.Format("Http://{0}/{1}/{2}/{3}",
                                 mPrefix,
                                 ExportHelper.RootPathName,
                                 mExportHelper.CurrentDirectoryName,
                                 FileName);
        }

        public System.Data.DataTable ReadExcelData(string Fullpath)
        {
            DataTable dtExcel = new System.Data.DataTable();
            var excelfs = new FileStream(Fullpath, FileMode.Open, FileAccess.Read);
            var workbook = new XSSFWorkbook(excelfs);
            var sheet = (XSSFSheet)workbook.GetSheetAt(0);
            var headerRow = (XSSFRow)sheet.GetRow(0);
            //create columns
            var cellCount = headerRow.LastCellNum;
            for (var i = headerRow.FirstCellNum; i <= cellCount - 1; i++)
            {
                var column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                dtExcel.Columns.Add(column);
            }
            //create rows
            var rowCount = sheet.LastRowNum;
            for (var i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
            {
                var row = (XSSFRow)sheet.GetRow(i);
                if (row != null)
                {
                    var dataRow = dtExcel.NewRow();
                    for (var j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                            dataRow[j] = row.GetCell(j).ToString();
                    }
                    dtExcel.Rows.Add(dataRow);
                }
            }
            //'dispose resources
            excelfs.Close();
            excelfs.Dispose();
            workbook = null;
            sheet = null;
            return dtExcel;
        }

        public string RootPathName
        {
            get
            {
                return ExportHelper.RootPathName;
            }
            set
            {
                ExportHelper.RootPathName = value;
            }
        }


        public string AppName
        {
            get
            {
                return ExportHelper.AppName;
            }
            set
            {
                ExportHelper.AppName = value;
            }
        }
    }
}
