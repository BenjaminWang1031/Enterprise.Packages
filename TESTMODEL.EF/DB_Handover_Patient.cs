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
    
    public partial class DB_Handover_Patient
    {
        public System.Guid DHP_ID { get; set; }
        public Nullable<int> DHP_TYPE { get; set; }
        public string DHP_USERTYPE { get; set; }
        public Nullable<decimal> DHP_Validity { get; set; }
        public string DHP_NAME { get; set; }
        public string DHP_SEX { get; set; }
        public Nullable<int> DHP_AGE { get; set; }
        public string DHP_BL_NUM { get; set; }
        public string DHP_CHECK_NUM { get; set; }
        public string DHP_MOBILE { get; set; }
        public string DHP_Diagnosis { get; set; }
        public string DHP_Urgency { get; set; }
        public string DHP_CONTENT { get; set; }
        public Nullable<System.Guid> CREATOR { get; set; }
        public Nullable<System.DateTime> CREATEDATE { get; set; }
        public Nullable<System.Guid> MODIFY { get; set; }
        public Nullable<System.DateTime> MODIFYDATE { get; set; }
    }
}
