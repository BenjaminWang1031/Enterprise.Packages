//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TESTMODEL.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class DB_TRAIN_FOREIGN
    {
        public System.Guid DT_ID { get; set; }
        public Nullable<System.Guid> DT_DUI_ID { get; set; }
        public Nullable<System.DateTime> DT_BEGIN { get; set; }
        public Nullable<System.DateTime> DT_END { get; set; }
        public Nullable<bool> DT_END_NOW { get; set; }
        public string DT_Project_Name { get; set; }
        public string DT_Project_Code { get; set; }
        public string DT_Host_Unit { get; set; }
        public string DT_Fee_Support { get; set; }
        public string DT_Country_Id { get; set; }
        public string DT_Island_Id { get; set; }
        public Nullable<System.Guid> DT_ExamResults_Id { get; set; }
        public Nullable<System.Guid> CREATOR { get; set; }
        public Nullable<System.DateTime> CREATEDATE { get; set; }
        public Nullable<System.Guid> MODIFY { get; set; }
        public Nullable<System.DateTime> MODIFYDATE { get; set; }
        public Nullable<int> DT_Status { get; set; }
        public string DT_BOOK { get; set; }
        public string DT_DATE_SUM { get; set; }
        public string DT_HASBOOK { get; set; }
        public Nullable<int> DT_ORDER { get; set; }
    }
}
