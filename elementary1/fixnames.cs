using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eit_tools;

using System.IO;

namespace elementary1
{
    class fixnames
    {
        public static string fixRecordingFilename(string sFullname)
        {
            string sRet = "";
            string sPath = Path.GetDirectoryName(sFullname);
            if (!sPath.EndsWith("\\"))
                sPath += "\\";
            string fileName = Path.GetFileNameWithoutExtension(sFullname);
            if(File.Exists(sPath + fileName+".eit")){
                eit_tools.eit_file.eit eitInfo = new eit_tools.eit_file.eit();
                eitInfo.readStreamData(sPath + fileName + ".eit");
            }
            return sRet;
        }
    }
}
