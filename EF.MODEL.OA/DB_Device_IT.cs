//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EF.MODEL.OA
{
    using System;
    using System.Collections.Generic;
    
    public partial class DB_Device_IT
    {
        public System.Guid DDI_ID { get; set; }
        public string DDI_Number { get; set; }
        public Nullable<System.Guid> DDI_Type_ID { get; set; }
        public string DDI_Model { get; set; }
        public Nullable<System.Guid> DDI_Location_ID { get; set; }
        public Nullable<System.Guid> DDI_Run_State_ID { get; set; }
        public string DDI_Two_Dimension_Code { get; set; }
        public string DDI_IP { get; set; }
        public Nullable<decimal> DDI_Price { get; set; }
        public Nullable<System.DateTime> DDI_Buy_Date { get; set; }
        public Nullable<System.DateTime> DDI_Guarantee_Time_Begin { get; set; }
        public Nullable<System.DateTime> DDI_Guarantee_Time_End { get; set; }
        public Nullable<System.DateTime> DDI_Expect_Scrap_Date { get; set; }
        public Nullable<System.DateTime> DDI_Scrap_Date { get; set; }
        public Nullable<int> DDI_Order { get; set; }
        public Nullable<System.Guid> CREATOR { get; set; }
        public Nullable<System.DateTime> CREATEDATE { get; set; }
        public Nullable<System.Guid> MODIFY { get; set; }
        public Nullable<System.DateTime> MODIFYDATE { get; set; }
    }
}
