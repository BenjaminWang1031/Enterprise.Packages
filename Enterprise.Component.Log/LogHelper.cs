using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Web;
using Enterprise.Core.Interface.Log;
using Enterprise.Core.Interface.Common;
using log4net;
using log4net.Config;

//Load configuration from web.config file
//TODO: Log component:To be flexible...
[assembly: XmlConfigurator(ConfigFile = "web.config", Watch = true)]
namespace Enterprise.Component.Log4Net
{
    /// <summary>
    /// Log4net basic class
    /// @Author:Benjamin Wang
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// global log4net instance
        /// </summary>
        private static ILog mLog4NetNative;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="T"></param>
        public LogHelper(Type T)
        {
            if (mLog4NetNative == null)
            {
                Init();
                mLog4NetNative = log4net.LogManager.GetLogger(T);
            }
        }


        #region Logger public member
        
       
        /// <summary>
        /// Fatal Level log
        /// </summary>
        /// <param name="Content"></param>
        public void LogFatal(string Content)
        {
            mLog4NetNative.Fatal(Content);
        }

        /// <summary>
        /// Error level log
        /// </summary>
        /// <param name="Content"></param>
        public void LogError(string Content)
        {
            mLog4NetNative.Error(Content);
        }

        /// <summary>
        /// Warn level log
        /// </summary>
        /// <param name="Content"></param>
        public void LogWarn(string Content)
        {
            mLog4NetNative.Warn(Content);
        }

        /// <summary>
        /// Info level log
        /// </summary>
        /// <param name="Content"></param>
        public void LogInfo(string Content)
        {
            mLog4NetNative.Info(Content);
        }

        /// <summary>
        /// Debug level log
        /// </summary>
        /// <param name="Content"></param>
        public void LogDebug(string Content)
        {
            mLog4NetNative.Debug(Content);
        }

        #endregion

        #region Static Method    
        
        /// <summary>
        /// Init log configure
        /// </summary>
        public static void Init()
        {
            if (mLog4NetNative != null) return;
            var mSection = ConfigurationManager.GetSection("log4net");
            if (mSection != null)
                XmlConfigurator.Configure();
            else
                BasicConfigurator.Configure();
        }

        /// <summary>
        /// Pack logs and return a full url
        /// @Author:Benjamin Wang
        /// </summary>
        /// <param name="ZipHelper"></param>
        /// <param name="LogFolder"></param>
        /// <returns></returns>
        public static string PackLogs(IZipHelper ZipHelper, string LogFolder)
        {
            try
            {
                var list = new List<string>();
                list.Add("zip");

                var mFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".zip";
                ZipHelper.ZipFolder(LogFolder, LogFolder + "//" + mFileName, false, list);
                var urlAuthority = System.Web.HttpContext.Current.Request.Url.Authority;
                return "Logs were packed:[" + String.Format("<a href='Http://{0}/{1}/{2}'>{2}</a>", urlAuthority, "Log", mFileName) + "]";
            }
            catch (Exception ex)
            {
                var helper = new LogExceptionHelper();
                helper.LogException(System.Reflection.MethodBase.GetCurrentMethod(), ex, null);
                return "";
            }
        }
        #endregion
    }
}
