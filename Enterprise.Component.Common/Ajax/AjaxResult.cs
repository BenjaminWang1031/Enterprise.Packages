
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enterprise.Component.Common.Ajax
{
    public class AjaxResult
    {
        #region Private Member

        private bool mSuccessfully = true;
        private bool mIsTimeOut = false;
        private string mMessage;
        private string mData;
        private string mTotalCount;

        #endregion

        /// <summary>
        /// Method
        /// </summary>
        public AjaxResult() 
        {
            //
        }

        #region Error
        public static AjaxResult ReturnError()
        {
            return new AjaxResult()
            {
                mSuccessfully = false,
                mMessage = "Error occured, please try again later..."
            };
        }

        public static AjaxResult ReturnError(string message)
        {
            return new AjaxResult()
            {
                mSuccessfully = false,
                Message = message
            };
        }
        #endregion

        #region Success
        public static AjaxResult ReturnSuccess()
        {
            return new AjaxResult()
            {
                mSuccessfully = true
            };
        }
        public static AjaxResult ReturnSuccess(string data, string totalCount = "")
        {
            return new AjaxResult()
            {
                mSuccessfully = true,
                Data = data,
                TotalCount = totalCount
            };
        }


        #endregion

        #region TimeOut
        public static AjaxResult ReturnTimeOut()
        {
            return new AjaxResult()
            {
                mSuccessfully = false,
                mIsTimeOut = true
            };
        }
        #endregion

        #region Public Member

        public bool Successfully
        {
            get { return mSuccessfully; }
        }

        public bool IsTimeOut
        {
            get { return mIsTimeOut; }
            set { this.mIsTimeOut = value; }
        }

        public string Message
        {
            get { return this.mMessage; }
            set { this.mMessage = value; }
        }

        public string Data
        {
            get { return this.mData; }
            set { this.mData = value; }
        }

        public string TotalCount
        {
            get { return this.mTotalCount; }
            set { this.mTotalCount = value; }
        }
        #endregion

    }
}
