using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enterprise.Core.Interface.Log;
using Enterprise.Core.Interface.Common;

namespace Enterprise.Component.Log4Net
{
    /// <summary>
    /// Log user activities
    /// @Author:Benjamin Wang
    /// </summary>
    public class LogUserHelper: ILogUserHelper
    {
        /// <summary>
        /// Log user
        /// @Author:Benjamin Wang
        /// </summary>
        /// <param name="MoudleInfo"></param>
        /// <param name="CurrentUserInfo"></param>
        public void LogUser(string MoudleInfo, UserInfo CurrentUserInfo)
        {
            var log = new LogHelper(this.GetType());
            var sb = new StringBuilder();
            sb.Append(String.Format("{0},", DateTime.Now.ToString("MM/dd/yyyy")));
            sb.Append(String.Format("{0},", DateTime.Now.ToString("HH:mm:ss")));
            sb.Append(String.Format("{0},", ReplaceDot(CurrentUserInfo.UserName)));
            sb.Append(String.Format("{0},", ReplaceDot(MoudleInfo)));
            sb.Append(String.Format("{0},", ReplaceDot(CurrentUserInfo.Country)));
            sb.Append(String.Format("{0},", ReplaceDot(CurrentUserInfo.Region)));
            sb.Append(String.Format("{0},", ReplaceDot(CurrentUserInfo.City)));
            sb.Append(String.Format("{0},", ReplaceDot(CurrentUserInfo.Org)));
            sb.Append(String.Format("{0},", ReplaceDot(CurrentUserInfo.Ip)));
            sb.Append(String.Format("{0}", CurrentUserInfo.UserId));
            //To use the info level logger of log4net
            log.LogInfo(sb.ToString());
        }

        /// <summary>
        /// @Author:Benjamin Wang
        /// @Dec: To replace ',' to '.'
        /// </summary>
        /// <param name="Info"></param>
        /// <returns></returns>
        private string ReplaceDot(string Info)
        { 
            return string.IsNullOrEmpty(Info)?"":Info.Replace(",", ".");
        }

        /// <summary>
        /// Pack logs
        /// @Author:Benjamin Wang
        /// </summary>
        /// <param name="ZipHelper"></param>
        /// <param name="LogFolder"></param>
        /// <returns></returns>
        public string PackLogs(IZipHelper ZipHelper, string LogFolder,string AppName="")
        {
            return LogHelper.PackLogs(ZipHelper, LogFolder, AppName);
        }
    }
}
