using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.Core.Interface.Common
{
    public interface IZipHelper
    {
        void ZipFolder(string ZipFromPath, string SaveToFullPath, bool IncludeAllFiles = true, List<string> ExcludeFileTypes = null);

    }
}
