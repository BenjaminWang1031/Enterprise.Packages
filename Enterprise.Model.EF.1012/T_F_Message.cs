//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Enterprise.Model.EF._1012
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_F_Message
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string SenderID { get; set; }
        public string ReceiverID { get; set; }
        public System.DateTime SendTime { get; set; }
        public Nullable<System.DateTime> ReadTime { get; set; }
        public int Type { get; set; }
        public string MainRecordID { get; set; }
        public int Importance { get; set; }
        public Nullable<bool> IsRead { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
    }
}
