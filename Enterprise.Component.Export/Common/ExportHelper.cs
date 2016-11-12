using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using System.IO;

namespace Enterprise.Component.Export.Common
{
    public class ExportHelper
    {
        public string CurrentDirectoryName;
        public static string RootPathName;
        public static string AppName;
        public string DestFolder;

        public ExportHelper()
        {
            CurrentDirectoryName = DateTime.Now.ToString("yyyyMMdd");
            //Create folder for the destination file
            DestFolder = GetDirectory().FullName;
        }

        private void CheckRootFolder()
        {
            var strRootPath = HttpContext.Current.Server.MapPath("~/" + RootPathName);
            if (!Directory.Exists(strRootPath))
                Directory.CreateDirectory(strRootPath);
        }

        private DirectoryInfo GetDirectory()
        {
            var strRootPath = HttpContext.Current.Server.MapPath("~/" + RootPathName);
            if (!Directory.Exists(strRootPath))
                Directory.CreateDirectory(strRootPath);
            //clear dics
            CleanOldDirectory(strRootPath);
            //create Directory
            var strDirectoryPath = strRootPath + "/" + CurrentDirectoryName;
            if (Directory.Exists(strDirectoryPath))
                return new DirectoryInfo(strDirectoryPath);
            return Directory.CreateDirectory(strDirectoryPath);
        }

        private void CleanOldDirectory(string strRootPath)
        {
            var strDivList = Directory.GetDirectories(strRootPath);
            if (strDivList.Length > 0)
            {
                foreach (var strDiv in strDivList)
                {
                    if (!strDiv.Contains(CurrentDirectoryName))
                        Directory.Delete(strDiv, true);
                }
            }
        }
    }
}
