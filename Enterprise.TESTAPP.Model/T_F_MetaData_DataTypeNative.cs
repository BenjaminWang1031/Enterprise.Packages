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
    
    public partial class T_F_MetaData_DataTypeNative
    {
        public int DataTypeID { get; set; }
        public string Name { get; set; }
        public string ActualType { get; set; }
        public string SystemType { get; set; }
        public string NHibernateType { get; set; }
        public bool NeedLength { get; set; }
        public bool NeedPrecision { get; set; }
        public bool NeedUnique { get; set; }
        public bool NeedRequired { get; set; }
        public bool NeedVisible { get; set; }
        public bool CanSetPrimaryKey { get; set; }
        public bool CanSetValue { get; set; }
        public string Description { get; set; }
        public int RecordNum { get; set; }
        public bool NeedSupportMultiLanguage { get; set; }
    }
}
