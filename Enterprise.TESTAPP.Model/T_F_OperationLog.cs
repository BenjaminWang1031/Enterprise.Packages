//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Enterprise.EF.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_F_OperationLog
    {
        public string ID { get; set; }
        public string SystemID { get; set; }
        public string SystemName { get; set; }
        public string ModuleID { get; set; }
        public string ModuleName { get; set; }
        public string LogDescribe { get; set; }
        public string LogType { get; set; }
        public string IPAddress { get; set; }
        public string SourceTable { get; set; }
        public string SourceGUID { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string CreatePersonAccount { get; set; }
        public string CreatePersonName { get; set; }
    }
}
