using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using Enterprise.Core.Interface.Export;


namespace Enterprise.TestApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public string Test()
        {
            IExportToExcelHelper exportHelper = Factory.MyFactory.GetExportToExcelHelper();  //new Enterprise.Component.Export.Excel.NPOIExcelExpoter("ExcelExport");
            var tab = new DataTable();
            tab.Columns.Add("A");
            tab.Columns.Add("B");
            tab.Columns.Add("C");

            for (var i = 0; i <= 100; i++)
            {
                var row = tab.NewRow();
                row[0] = string.Format("row:{0} col{1}",i,0);
                row[1] = string.Format("row:{0} col{1}", i, 1);
                row[2] = string.Format("row:{0} col{1}", i, 2);
                tab.Rows.Add(row);
            }

            return exportHelper.ExportToExcel ("TEST","BENJAMIN",tab);
        }
    }
}
