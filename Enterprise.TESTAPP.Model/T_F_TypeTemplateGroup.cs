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
    
    public partial class T_F_TypeTemplateGroup
    {
        public T_F_TypeTemplateGroup()
        {
            this.T_F_TypeTemplate = new HashSet<T_F_TypeTemplate>();
        }
    
        public string ID { get; set; }
        public string Name { get; set; }
        public string FID { get; set; }
        public string Type { get; set; }
        public string Remark { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string IsDel { get; set; }
        public int LevelNum { get; set; }
    
        public virtual ICollection<T_F_TypeTemplate> T_F_TypeTemplate { get; set; }
    }
}
