using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Enterprise.Core.Interface.Log;
using Enterprise.Core.Interface.Common;

namespace Enterprise.Component.Log4Net
{
    /// <summary>
    /// Exception handler
    /// @Author:Benjamin Wang
    /// </summary>
    public class LogExceptionHelper : ILogExceptionHelper
    {
        /// <summary>
        /// Log exception
        /// @Author:Benjamin Wang
        /// </summary>
        /// <param name="MethodInfo"></param>
        /// <param name="Exception"></param>
        /// <param name="CurrentUserInfo"></param>
        public void LogException(MethodBase MethodInfo, Exception Exception, UserInfo CurrentUserInfo)
        {
            LogException(MethodInfo.ReflectedType.FullName, MethodInfo.Name, Exception, CurrentUserInfo);
        }

        /// <summary>
        /// Log exception
        /// @Author:Benjamin Wang
        /// </summary>
        /// <param name="FullClassName"></param>
        /// <param name="MethodName"></param>
        /// <param name="Exception"></param>
        /// <param name="CurrentUserInfo"></param>
        public void LogException(string FullClassName, string MethodName, Exception Exception, UserInfo CurrentUserInfo)
        {
            var log = new LogHelper(this.GetType());
            var sb = new StringBuilder();
            sb.AppendLine(String.Format("Time:{0}", DateTime.Now));
            sb.AppendLine(String.Format("UserName:{0}", CurrentUserInfo == null ? "N/A" : CurrentUserInfo.UserName));
            sb.AppendLine(String.Format("ClassName:{0}", FullClassName));
            sb.AppendLine(String.Format("MethodName:{0}", MethodName));
            sb.AppendLine(String.Format("LineNumber:{0}", GetCurrentLineNumber(Exception)));
            sb.AppendLine(String.Format("ErrorMessage:{0}", Exception.Message));
            //To use the error level logger of log4net
            log.LogError(sb.ToString());
        }

        /// <summary>
        /// Get exception line number
        /// @Author:Benjamin Wang
        /// </summary>
        /// <param name="Exception"></param>
        /// <returns></returns>
        public string GetCurrentLineNumber(Exception Exception)
        {
            if (Exception.StackTrace == null)
                return "Not original exception, please check the source codes.";
            try
            {
                var lineNumber = 0;
                var lineSearch = ":line ";
                var index = Exception.StackTrace.LastIndexOf(lineSearch);
                if (index != -1)
                {
                    var lineNumberText = Exception.StackTrace.Substring(index + lineSearch.Length);
                    if (Int32.TryParse(lineNumberText, out lineNumber)) { }
                    else 
                        return lineNumberText.Contains("\r\n") ? lineNumberText.Substring(0, lineNumberText.IndexOf("\r\n")) : lineNumberText;
                }
                return lineNumber.ToString();
            }
            catch (Exception ex)
            {
                return "OOPS...got new exception [" + ex.Message+"] when try to get exception line... please check the source codes.";
            }
        }

        /// <summary>
        /// Pack logs
        /// @Author:Benjamin Wang
        /// </summary>
        /// <param name="ZipHelper"></param>
        /// <param name="LogFolder"></param>
        /// <returns></returns>
        public string PackLogs(IZipHelper ZipHelper, string LogFolder, string AppName="")
        {
            return LogHelper.PackLogs(ZipHelper, LogFolder, AppName);
        }
    }
}
