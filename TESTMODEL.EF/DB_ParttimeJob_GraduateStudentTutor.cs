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
    
    public partial class DB_ParttimeJob_GraduateStudentTutor : Enterprise.Core.Interface.Data.IEntityRoot
    {
        public System.Guid DP_ID { get; set; }
        public Nullable<System.Guid> DP_DUI_ID { get; set; }
        public string DP_School { get; set; }
        public Nullable<System.Guid> DP_TutorType_Id { get; set; }
        public Nullable<System.DateTime> DP_Begin { get; set; }
        public Nullable<System.DateTime> DP_End { get; set; }
        public Nullable<bool> DP_End_Now { get; set; }
        public Nullable<System.Guid> CREATOR { get; set; }
        public Nullable<System.DateTime> CREATEDATE { get; set; }
        public Nullable<System.Guid> MODIFY { get; set; }
        public Nullable<System.DateTime> MODIFYDATE { get; set; }
        public Nullable<int> DP_Status { get; set; }
        public string DP_BOOK { get; set; }
        public Nullable<int> DP_ORDER { get; set; }
    }
}
