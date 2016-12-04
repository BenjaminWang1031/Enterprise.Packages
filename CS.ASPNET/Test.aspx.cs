using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enterprise.Component.EntityFramework;
using TESTMODEL.EF;
 

namespace CS.ASPNET
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RunEFTest();
            }
        }

        /// <summary>
        /// To test the component built with entityframework
        /// </summary>
        private void RunEFTest()
        {
            var helper = new DBManager("HospitalOAEntities");
            //add
            //for(var i=1;i<4;i++)
            //{ 
            //    var entity = new DB_USER_INFO();
            //    entity.DUI_ID = Guid.NewGuid();
            //    entity.DUI_NAME = "Benjamin"+i.ToString();
            //    helper.Add(entity);
            //}
            //helper.Commit();
            //Response.Write("Done! <br/>");

            //query single by key
            //var qEntity = helper.GetEntity<DB_USER_INFO>(Guid.Parse("DAE856AA-2586-49FC-823A-006BAB83AC9E"));

            //query single by property
            //with ef 
            //var efHelper = new HospitalOAEntities();
            //var qEntity = efHelper.DB_USER_INFO.Where(e => e.DUI_NAME == "benjamin").First();
            //with component
            //var qEntity = helper.GetEntity<DB_USER_INFO>(item => item.DUI_NAME == "benjamin");
            //if (qEntity != null)
            //    Response.Write(qEntity.DUI_NAME + "&nbsp;|&nbsp;" + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss"));
            //else
            //    Response.Write("Not found");
            //Response.Write("<br/>");

            //delete
            var qEntity = helper.GetEntity<DB_USER_INFO>(item => item.DUI_NAME == "benjamin");
            if (qEntity != null)
            {
                helper.Delete(qEntity);
                helper.Commit();
                Response.Write(qEntity.DUI_NAME + " is deleted now.");
            }
            else
                Response.Write("Entity was not found");
            Response.Write("<br/>");

            //query list
            var list = helper.GetEntityList<DB_USER_INFO>(item => item.DUI_NAME.Contains("benjamin")).OrderBy(item=>item.DUI_NAME).ToList();
            foreach (var user in list)
            {
                Response.Write(user.DUI_NAME);
                Response.Write("<br/>");
            }
            

            //var list1 = helper.GetEntityList<DSP_Sensor>();
            //Response.Write(list1[2].Sensor_Name);
            //Response.Write("<br/>");

            //var list2 = helper.GetEntityList<DSP_Sensor>(item => item.Sensor_ID > 0);
            //Response.Write(list2[2].Sensor_Name);
        }
    }
}