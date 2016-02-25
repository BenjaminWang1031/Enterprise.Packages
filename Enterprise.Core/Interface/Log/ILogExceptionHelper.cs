using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;


namespace Enterprise.Core.Interface.Log
{
    /// <summary>
    /// Log exception helper
    /// </summary>
    public interface ILogExceptionHelper : ILogHelper
    {
        /// <summary>
        /// Log exception by reflected class/method info
        /// </summary>
        /// <param name="MethodInfo"></param>
        /// <param name="Exception"></param>
        void LogException(MethodBase MethodInfo, Exception Exception, UserInfo CurrentUserInfo);

        /// <summary>
        /// Log exception by specified class info
        /// </summary>
        /// <param name="FullClassName"></param>
        /// <param name="MethodName"></param>
        /// <param name="Exception"></param>
        void LogException(string FullClassName, string MethodName, Exception Exception, UserInfo CurrentUserInfo);

        /// <summary>
        /// Get Current line number
        /// </summary>
        /// <param name="Exception"></param>
        /// <returns></returns>
        string GetCurrentLineNumber(Exception Exception);
    }
}
