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
    
    public partial class DB_Radiation_TrainCheck
    {
        public System.Guid DRT_ID { get; set; }
        public Nullable<System.DateTime> DRT_Date { get; set; }
        public string DRT_Organ { get; set; }
        public string DRT_Result { get; set; }
        public Nullable<System.Guid> CREATOR { get; set; }
        public Nullable<System.DateTime> CREATEDATE { get; set; }
        public Nullable<System.Guid> MODIFY { get; set; }
        public Nullable<System.DateTime> MODIFYDATE { get; set; }
        public Nullable<int> DRT_Order { get; set; }
        public Nullable<int> Status { get; set; }
    }
}
