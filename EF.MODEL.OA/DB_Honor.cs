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
    
    public partial class DB_Honor
    {
        public System.Guid DH_ID { get; set; }
        public Nullable<System.Guid> DH_DUI_ID { get; set; }
        public string DH_Name { get; set; }
        public Nullable<System.DateTime> DH_Get_Date { get; set; }
        public Nullable<System.Guid> DH_AwardWinningLevel_Id { get; set; }
        public string DH_Award_Unit { get; set; }
        public string DH_Peaple_Count { get; set; }
        public string DH_Ranking { get; set; }
        public string DH_Certificate { get; set; }
        public Nullable<System.Guid> CREATOR { get; set; }
        public Nullable<System.DateTime> CREATEDATE { get; set; }
        public Nullable<System.Guid> MODIFY { get; set; }
        public Nullable<System.DateTime> MODIFYDATE { get; set; }
        public Nullable<int> DH_Status { get; set; }
        public Nullable<int> DH_ORDER { get; set; }
    }
}
