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
    
    public partial class DB_Position_State
    {
        public System.Guid DOS_ID { get; set; }
        public string POSITION { get; set; }
        public Nullable<decimal> WORKLOAD { get; set; }
        public Nullable<decimal> AMT { get; set; }
        public Nullable<int> YEAR { get; set; }
        public Nullable<int> MONTH { get; set; }
        public Nullable<System.DateTime> CREATEDATE { get; set; }
        public Nullable<System.Guid> CREATOR { get; set; }
    }
}
