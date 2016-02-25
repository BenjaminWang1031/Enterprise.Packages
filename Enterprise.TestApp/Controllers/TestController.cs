using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Reflection;
using System.Text;

using Enterprise.Core.Interface.Data;
using Enterprise.Core.Interface.Log;
using Enterprise.Component.Log4Net;
using Enterprise.Component.Nhiberate;
using Enterprise.Component.Common.Utility;
using Enterprise.Component.Nhiberate.Base;
using Enterprise.TestApp.Factory;
using Enterprise.NH.Model;
using Enterprise.EF.Model;

namespace Enterprise.TestApp.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            //TestExcel();
            //TestAddData();
            //TestQueryData();

            //var helper = new NHTestHelper(this);
            //helper.TestAdd();
            //helper.TestUpdate();
            //helper.TestQuery();
            //helper.TestPaged();
            //helper.TestSQL();
            //helper.TestQueryAll();
            //helper.TestProcedure();

            //var helper = new AdoNetHelper(this);
            //helper.TestQuery();
            //helper.TestProcedure();
            //var mZipHelper = new ZipHelper();
            //var mDesFolder = System.Web.HttpContext.Current.Server.MapPath("~/Log");
            //ViewBag.logsPath= MyFactory.GetExceptionLogHelper().PackLogs(mZipHelper, mDesFolder);
            MyFactory.GetExceptionLogHelper().LogException("", "", new Exception("TES--111T"), null);
            var user = new UserInfo();
            user.UserName = "Benjamin";
            MyFactory.GetLogVisitorHelper().LogUser("", user);
            return View();
        }
    }

    public class AdoNetHelper
    {
        Controller mCurrentController;

        public AdoNetHelper(Controller ctr)
        {
            mCurrentController = ctr;
        }

        public void TestQuery()
        {
            using (var mDBManager = new Enterprise.Component.AdoNet.DBManager())
            {
                var tab = mDBManager.GetDataTable("Select * From SYS_DEPT Where SD_ORDER=21");
                mCurrentController.ViewBag.Name = tab.Rows[0]["SD_NAME"].ToString();
            }
        }

        public void TestProcedure()
        {
            var mDBManager = new Enterprise.Component.AdoNet.DBManager();
            var name = "SP_GETDEPTLIST";
            var parms = new SqlParameter[]
                    {
                     new SqlParameter("@strName","TEST1")
                    };
            mCurrentController.ViewBag.QueryTable = mDBManager.GetDataTable(name, parms).Rows;
        }
    }

    public class NHTestHelper
    {
        Controller mCurrentController;

        public NHTestHelper(Controller ctr)
        {
            mCurrentController = ctr;
        }

        public void TestAdd()
        {
            using (var mDBManager = new DBManager())
            {
                for (var i = 11; i <= 100; i++)
                {
                    var entity = new SYS_DEPT();
                    entity.SD_ID = Guid.NewGuid();
                    entity.SD_NAME = "TEST" + i;
                    entity.SD_ORDER = i;
                    mDBManager.Add(entity);
                }
                mCurrentController.ViewBag.Message = mDBManager.Commit() ? "Ok" : mDBManager.ErrorMsg;
            }
        }

        public void TestUpdate()
        {
            using (var mDBManager = new DBManager())
            {
                var entity = mDBManager.Get<SYS_DEPT>(Guid.Parse("04CB793C-6866-4706-AAD2-0455BE430560"));
                if (entity != null)
                {
                    entity.SD_NAME = "Ben" + Guid.NewGuid().ToString();
                    mDBManager.Update(entity);
                    mDBManager.Commit();
                }
            }
        }

        public void TestQuery()
        {
            using (var mDBManager = new DBManager())
            {
                //get by key
                var entity = mDBManager.Get<SYS_DEPT>(Guid.Parse("04CB793C-6866-4706-AAD2-0455BE430560"));

                //get by spec
                var spec = Specification<SYS_DEPT>.Create(p => p.SD_NAME == "TEST1");
                //var entity = mDBManager.Get<SYS_DEPT>(spec);

                //var entity = mDBManager.GetAll<SYS_DEPT>(e=>e.SD_NAME, DBSortOrder.Descending)[0];

                //var spec = Specification<SYS_DEPT>.Create(p => p.SD_NAME.Contains("TEST5"));
                //var entity = mDBManager.GetAll<SYS_DEPT>(spec,e => e.SD_NAME, DBSortOrder.Descending)[0];

                mCurrentController.ViewBag.Name = entity == null ? "Nothing" : entity.SD_NAME;
            }
        }

        public void TestPaged()
        {
            using (var mDBManager = new DBManager())
            {
                var pageIndex = 2;
                var pageSize = 20;
                var totalCount = 0;

                var list = mDBManager.GetPaged<SYS_DEPT>(pageIndex, pageSize, e => e.SD_ORDER, DBSortOrder.Ascending);
                var sb = new StringBuilder();
                foreach (var entity in list)
                {
                    sb.AppendLine(entity.SD_NAME + "<br>");
                }
                mCurrentController.ViewBag.Name = sb.ToString();
            }
        }

        public void TestSQL()
        {
            using (var mDBManager = new DBManager())
            {
                //datatable
                var tab = mDBManager.GetDataTable("Select * From SYS_DEPT Where SD_ORDER=21");
                mCurrentController.ViewBag.Name = tab.Rows[0]["SD_NAME"].ToString();

                //var list = mDBManager.ExecuteSQLString<SYS_DEPT>(" Select * From SYS_DEPT Where SD_ORDER=21");
                //mCurrentController.ViewBag.Name = list.ToList()[0].SD_NAME;
            }
        }

        public void TestProcedure()
        {
            using (var mDBManager = new DBManager())
            {
                var name = "SP_GETDEPTLIST";
                var parms = new SqlParameter[]
                    {
                     new SqlParameter("@strName","TEST1")
                    };
                mCurrentController.ViewBag.QueryTable = mDBManager.GetDataTable(name, parms).Rows;
            }
        }

        public void TestQueryAll()
        {
            using (var mDBManager = new DBManager())
            {
                mCurrentController.ViewBag.QueryTable = mDBManager.GetAll<SYS_DEPT>();
            }
        }
    }

    public class EFTestHelper
    {
        Controller mCurrentController;

        public EFTestHelper(Controller ctr)
        {
            mCurrentController = ctr;
        }


        public void TestAddData()
        {
            using (var db = new EFLocalTest())
            {
                var model = new T_F_Demo();
                model.name = "TESTModel";
                model.des = "TESTModelDes";
                db.T_F_Demo.Add(model);
                mCurrentController.ViewBag.SaveResult = db.SaveChanges() > 0 ? "Successfully saved!" : "Error out!";
            }
        }

        public void TestDelData()
        {
            using (var db = new EFLocalTest())
            {
                var model = (from n in db.T_F_Demo where n.name == "test" select n).FirstOrDefault();
                db.T_F_Demo.Remove(model);
                mCurrentController.ViewBag.SaveResult = db.SaveChanges();
            }
        }

        private void TestQueryData()
        {
            using (var db = new EFLocalTest())
            {
                //ViewBag.QueryTable = db.T_F_Demo.SqlQuery("select * from T_F_Demo").ToList();
                mCurrentController.ViewBag.QueryTable = db.Database.SqlQuery<string>("select username from oasis").ToList();
            }
        }
    }

    public class ExportTestHelper
    {
        Controller mCurrentController;
        public ExportTestHelper(Controller ctr)
        {
            mCurrentController = ctr;
        }
        public void TestExcel()
        {
            //test exception
            var mLogExceptionHelper = MyFactory.GetExceptionLogHelper();
            mLogExceptionHelper.LogException(MethodBase.GetCurrentMethod(), new Exception("TEST...."), null);
            //test visitor
            var mLogVisitorHelper = MyFactory.GetLogVisitorHelper();
            var mVisitor = new UserInfo();
            mVisitor.UserId = "BBWANG";
            mVisitor.UserName = "BenjaminWang";
            mLogVisitorHelper.LogUser("Index", mVisitor);

            //test export
            var excelExportHelper = MyFactory.GetExportToExcelHelper();
            var tab = new DataTable();
            tab.Columns.Add(new DataColumn("A", typeof(string)));
            tab.Columns.Add(new DataColumn("B", typeof(string)));
            for (var i = 1; i <= 100; i++)
            {
                var row = tab.NewRow();
                row["A"] = string.Format("Row {0} Column{1}", i, 1);
                row["B"] = string.Format("Row {0} Column{1}", i, 2);
                tab.Rows.Add(row);
            }
            mCurrentController.ViewBag.DownloadLink = excelExportHelper.ExportToExcel("Test", "TEST-BEN", tab);
        }
    }
}
