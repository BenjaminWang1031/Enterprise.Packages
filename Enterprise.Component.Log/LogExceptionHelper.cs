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
    public class LogExceptionHelper : ILogExceptionHelper
    {
        public void LogException(MethodBase MethodInfo, Exception Exception, UserInfo CurrentUserInfo)
        {
            LogException(MethodInfo.ReflectedType.FullName, MethodInfo.Name, Exception, CurrentUserInfo);
        }

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
            log.LogWarn(sb.ToString());
        }

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
                    else return lineNumberText;
                }
                return lineNumber.ToString();
            }
            catch (Exception ex)
            {
                return "OOPS...got new exception when try to get exception line... please check the source codes.";
            }
        }

        public void Init() {
            LogHelper.Init();
        }

        public string PackLogs(IZipHelper ZipHelper, string LogFolder)
        {
            return LogHelper.PackLogs(ZipHelper, LogFolder);
        }
    }
}
