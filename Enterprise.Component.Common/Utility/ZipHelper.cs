using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Ionic.Zip;
using Enterprise.Core.Interface.Common;

namespace Enterprise.Component.Common.Utility
{
    public class ZipHelper:IZipHelper
    {
        public void ZipFolder(string ZipFromPath, string SaveToFullPath , 
                                    bool IncludeAllFiles=true, List<string> ExcludeFileTypes=null )
        {
            using (var myZip  = new ZipFile())
            {
                foreach (var file in Directory.GetFiles(ZipFromPath))
                {
                    if(IncludeAllFiles) 
                        myZip.AddFile(file, "");
                    else
                    {
                        if (ExcludeFileTypes != null )
                        { 
                            var found=false;
                            foreach(var extensionType in ExcludeFileTypes)
                            {
                                var myexten = file.Substring(file.LastIndexOf('.')+1, file.Length - file.LastIndexOf('.')-1).ToLower();
                                if (myexten == extensionType.ToLower())  found = true;
                            }
                            if(!found)  myZip.AddFile(file, "");
                        }
                        else  
                            myZip.AddFile(file, "");  
                    }
                    myZip.Save(SaveToFullPath);
                }
            }
        }
    }
}
