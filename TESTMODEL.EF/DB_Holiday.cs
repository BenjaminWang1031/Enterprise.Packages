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
    
    public partial class DB_Holiday : Enterprise.Core.Interface.Data.IEntityRoot
    {
        public System.Guid DH_ID { get; set; }
        public Nullable<System.DateTime> DH_BEGIN { get; set; }
        public Nullable<System.DateTime> DH_END { get; set; }
        public string DH_TYPE { get; set; }
        public string DH_TYPECODE { get; set; }
        public Nullable<decimal> DH_DAYS { get; set; }
        public string DH_REASON { get; set; }
        public Nullable<int> DH_STATE { get; set; }
        public Nullable<System.Guid> CREATOR { get; set; }
        public Nullable<System.DateTime> CREATEDATE { get; set; }
        public Nullable<System.Guid> MODIFY { get; set; }
        public Nullable<System.DateTime> MODIFYDATE { get; set; }
    }
}
