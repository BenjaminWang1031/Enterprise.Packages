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
    
    public partial class DB_SCHEDULING
    {
        public System.Guid DS_ID { get; set; }
        public Nullable<System.DateTime> DS_DATE { get; set; }
        public string DS_REMARK { get; set; }
        public Nullable<int> DS_STATE { get; set; }
        public Nullable<System.Guid> CREATOR { get; set; }
        public Nullable<System.DateTime> CREATEDATE { get; set; }
        public Nullable<System.Guid> MODIFY { get; set; }
        public Nullable<System.DateTime> MODIFYDATE { get; set; }
        public Nullable<System.Guid> DS_DSG_ID { get; set; }
        public string DS_WEEKREMARK { get; set; }
    }
}
