using System;
using Enterprise.Core.Interface.Data;

//Nhibernate Code Generation Template 1.0

namespace Enterprise.NH.Model
{
    //SYS_DEPT
    [Serializable]
    public class SYS_DEPT:IEntityRoot
    {

        /// <summary>
        /// SD_ID
        /// </summary>
        public virtual Guid SD_ID
        {
            get;
            set;
        }
        /// <summary>
        /// SD_NAME
        /// </summary>
        public virtual string SD_NAME
        {
            get;
            set;
        }
        /// <summary>
        /// SD_CODE
        /// </summary>
        public virtual string SD_CODE
        {
            get;
            set;
        }
        /// <summary>
        /// SD_PARENT_ID
        /// </summary>
        public virtual Guid? SD_PARENT_ID
        {
            get;
            set;
        }
        /// <summary>
        /// SD_ORDER
        /// </summary>
        public virtual decimal? SD_ORDER
        {
            get;
            set;
        }
        /// <summary>
        /// SPHD_LEVEL
        /// </summary>
        public virtual int? SD_LEVEL
        {
            get;
            set;
        }
        /// <summary>
        /// SPHD_FULL_NAME
        /// </summary>
        public virtual string SD_FULL_NAME
        {
            get;
            set;
        }
        /// <summary>
        /// SPHD_REMARK
        /// </summary>
        public virtual string SD_REMARK
        {
            get;
            set;
        }

        /// <summary>
        /// SD_MANAGER_ID
        /// </summary>
        public virtual Guid SD_MANAGER_ID
        {
            get;
            set;
        }

        /// <summary>
        /// SD_MANAGER_NAME
        /// </summary>
        public virtual string SD_MANAGER_NAME
        {
            get;
            set;
        }

        /// <summary>
        /// CREATOR
        /// </summary>
        public virtual Guid CREATOR
        {
            get;
            set;
        }
        /// <summary>
        /// CREATEDATE
        /// </summary>
        public virtual DateTime? CREATEDATE
        {
            get;
            set;
        }
        /// <summary>
        /// MODIFY
        /// </summary>
        public virtual Guid MODIFY
        {
            get;
            set;
        }
        /// <summary>
        /// MODIFYDATE
        /// </summary>
        public virtual DateTime? MODIFYDATE
        {
            get;
            set;
        }

        /// <summary>
        /// CONTACTS
        /// </summary>
        public virtual string CONTACTS
        {
            get;
            set;
        }

        /// <summary>
        /// CONTACTS_BACK
        /// </summary>
        public virtual string CONTACTS_BACK
        {
            get;
            set;
        }
    }
}