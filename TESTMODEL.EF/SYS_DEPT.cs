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
    
    public partial class SYS_DEPT : Enterprise.Core.Interface.Data.IEntityRoot
    {
        public System.Guid SD_ID { get; set; }
        public string SD_NAME { get; set; }
        public string SD_CODE { get; set; }
        public Nullable<System.Guid> SD_PARENT_ID { get; set; }
        public Nullable<double> SD_ORDER { get; set; }
        public Nullable<int> SD_LEVEL { get; set; }
        public string SD_FULL_NAME { get; set; }
        public string SD_REMARK { get; set; }
        public Nullable<System.Guid> SD_MANAGER_ID { get; set; }
        public string SD_MANAGER_NAME { get; set; }
        public Nullable<System.Guid> CREATOR { get; set; }
        public Nullable<System.DateTime> CREATEDATE { get; set; }
        public Nullable<System.Guid> MODIFY { get; set; }
        public Nullable<System.DateTime> MODIFYDATE { get; set; }
        public string CONTACTS { get; set; }
        public string CONTACTS_BACK { get; set; }
    }
}
