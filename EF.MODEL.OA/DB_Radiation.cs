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
    
    public partial class DB_Radiation
    {
        public System.Guid DR_ID { get; set; }
        public Nullable<System.Guid> DR_DUI_ID { get; set; }
        public Nullable<System.DateTime> DR_Examine_Date { get; set; }
        public string DR_Value { get; set; }
        public Nullable<System.Guid> CREATOR { get; set; }
        public Nullable<System.DateTime> CREATDATE { get; set; }
        public Nullable<System.Guid> DR_MODFIY { get; set; }
        public Nullable<System.DateTime> MODIFYDATE { get; set; }
        public Nullable<int> DR_Status { get; set; }
    }
}
