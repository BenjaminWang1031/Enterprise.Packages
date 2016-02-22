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

[assembly: XmlConfigurator(ConfigFile = "web.config", Watch = true)]
namespace Enterprise.Component.Log4Net
{
    public class LogHelper
    {
        private static ILog mLog4NetNative;

        public static void Init()
        {
            if (mLog4NetNative != null) return;
            var mSection = ConfigurationManager.GetSection("log4net");
            if (mSection != null)
                XmlConfigurator.Configure();
            else
                BasicConfigurator.Configure();
        }
        public LogHelper(Type T)
        {
            if (mLog4NetNative == null)
                mLog4NetNative = log4net.LogManager.GetLogger(T);
        }

        public void LogFatal(string Content)
        {
            mLog4NetNative.Fatal(Content);
        }

        public void LogError(string Content)
        {
            mLog4NetNative.Error(Content);
        }

        public void LogWarn(string Content)
        {
            mLog4NetNative.Warn(Content);
        }

        public static string PackLogs(IZipHelper ZipHelper, string LogFolder)
        {
            try
            {
                var list = new List<string>();
                list.Add("zip");

                var mFileName =  DateTime.Now.ToString("yyyyMMddHHmmss") + ".zip";
                ZipHelper.ZipFolder(LogFolder, LogFolder + "//" + mFileName, false, list);
                var urlAuthority = System.Web.HttpContext.Current.Request.Url.Authority;
                return "Logs were packed:[" + String.Format("<a href='Http://{0}/{1}/{2}'>{2}</a>", urlAuthority, "Log", mFileName) + "]";
            }
            catch (Exception ex)
            {
                var helper = new LogExceptionHelper();
                helper.LogException(System.Reflection.MethodBase.GetCurrentMethod(),  ex,null);
                return "";
            }

        }
    }
}
