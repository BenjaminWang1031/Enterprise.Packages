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
    
    public partial class DB_Radiation_BasicInfo : Enterprise.Core.Interface.Data.IEntityRoot
    {
        public System.Guid DRB_ID { get; set; }
        public string DRB_Number { get; set; }
        public string DRB_Organ { get; set; }
        public Nullable<System.DateTime> DRB_Date { get; set; }
        public Nullable<System.Guid> DRB_Code_Id { get; set; }
        public Nullable<System.Guid> CREATOR { get; set; }
        public Nullable<System.DateTime> CREATEDATE { get; set; }
        public Nullable<System.Guid> MODIFY { get; set; }
        public Nullable<System.DateTime> MODIFYDATE { get; set; }
        public Nullable<int> DRB_Order { get; set; }
        public Nullable<int> Status { get; set; }
    }
}
