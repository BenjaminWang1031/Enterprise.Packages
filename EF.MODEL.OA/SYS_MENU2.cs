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
    
    public partial class SYS_MENU2
    {
        public System.Guid SM_ID { get; set; }
        public string SM_NAME { get; set; }
        public string SM_CODE { get; set; }
        public string SM_URL { get; set; }
        public Nullable<System.Guid> SM_PARENT_ID { get; set; }
        public Nullable<int> SM_LEVEL { get; set; }
        public string SM_OPEN_NEW { get; set; }
        public Nullable<bool> SM_ENABLED { get; set; }
        public Nullable<int> SM_ORDER { get; set; }
        public Nullable<System.Guid> CREATOR { get; set; }
        public Nullable<System.DateTime> CREATEDATE { get; set; }
        public Nullable<System.Guid> MODIFY { get; set; }
        public Nullable<System.DateTime> MODIFYDATE { get; set; }
    }
}
