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
    
    public partial class DB_Audit : Enterprise.Core.Interface.Data.IEntityRoot
    {
        public System.Guid DA_ID { get; set; }
        public Nullable<System.Guid> DA_DUI_ID { get; set; }
        public Nullable<System.DateTime> DA_Finish_Date { get; set; }
        public string DA_Desc { get; set; }
        public Nullable<System.Guid> CREATOR { get; set; }
        public Nullable<System.DateTime> CREATEDATE { get; set; }
        public Nullable<System.Guid> MODIFY { get; set; }
        public Nullable<System.DateTime> MODIFYDATE { get; set; }
        public Nullable<int> DA_Count_Unfinish { get; set; }
        public Nullable<int> DA_Count_Finish { get; set; }
        public Nullable<bool> DA_IKNOW { get; set; }
    }
}
