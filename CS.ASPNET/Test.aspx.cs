using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enterprise.Component.EntityFramework;
//using Enterprise.EF.Model; 
using TESTMODEL.EF;

namespace CS.ASPNET
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var helper = new DBManager("LocalTestDBEntities");
                //var entity = new DSP_Pilot();
                //entity.Pilot_Name = "benjamin";
                //helper.Add(entity);
                //helper.Commit();

                var list = helper.GetEntityList<DSP_Pilot>();
                foreach (var dsp in list)
                {
                    Response.Write(dsp.Pilot_Name);
                    Response.Write("<br/>");
                }
                //var entity = helper.GetEntity<DSP_Sensor>(item => item.Sensor_ID == 3);
                //Response.Write(entity.Sensor_Name);
                //Response.Write("<br/>");

                //var list1 = helper.GetEntityList<DSP_Sensor>();
                //Response.Write(list1[2].Sensor_Name);
                //Response.Write("<br/>");

                //var list2 = helper.GetEntityList<DSP_Sensor>(item => item.Sensor_ID > 0);
                //Response.Write(list2[2].Sensor_Name);
            }
        }
    }
}