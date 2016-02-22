using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enterprise.Core.Interface.Log;
using Enterprise.Core.Interface.Common;

namespace Enterprise.Component.Log4Net
{
    public class LogUserHelper: ILogUserHelper
    {
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
            log.LogError(sb.ToString());
        }

        private string ReplaceDot(string Info)
        { 
            return string.IsNullOrEmpty(Info)?"":Info.Replace(",", ".");
        }

        public void Init() {
            LogHelper.Init();
        }

        public string PackLogs(IZipHelper ZipHelper, string LogFolder)
        {
            return LogHelper.PackLogs(ZipHelper, LogFolder );
        }
    }
}
