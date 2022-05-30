using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Medley.Common.Logging;

namespace Medley.Common.Utils
{
    public class FileUtils
    {
        /// <summary>
        /// Move files with a specific search pattern from one directory to another.
        /// </summary>
        /// <param name="sourceDir"></param>
        /// <param name="destDir"></param>
        /// <param name="searchOptions"></param>
        public static bool MoveFilesToDir(string sourceDir, string destDir, string searchOptions)
        {
            try
            {
                if (!Directory.Exists(sourceDir))
                {
                    MedleyLogger.Instance.Error(string.Format("Source directory {0} do not exist while trying to move files", sourceDir));
                    return false;
                }

                string[] aFiles = Directory.GetFiles(sourceDir, searchOptions, SearchOption.TopDirectoryOnly);

                if (aFiles.Length == 0)
                    return true;

                if (!Directory.Exists(destDir))
                    Directory.CreateDirectory(destDir);

                foreach (string strFilePathAndName in aFiles)
                {
                    int i = strFilePathAndName.LastIndexOf('\\');
                    string strFileName = strFilePathAndName.Substring(i + 1, strFilePathAndName.Length - i - 1);
                    File.Move(strFilePathAndName, destDir + strFileName);
                } 
            }
            catch(Exception ex)
            {
                MedleyLogger.Instance.Error("Failed to move files", ex);
                return false;
            }

            return true;
        }

        public static void WriteXmlFileToDir(string strDir, string strFilename, string strText)
        {
            try
            {
                if (!Directory.Exists(strDir))
                    Directory.CreateDirectory(strDir);

                if (!strDir.EndsWith(@"\"))
                    strDir += @"\";

                string strFullFilename = string.Format("{0}{1}{2:ddMMyyyy_HHmmss_FFF}.xml", strDir, strFilename, DateTime.Now);

                File.WriteAllText(strFullFilename, strText);

            }
            catch (Exception ex)
            {
                MedleyLogger.Instance.Error("Failed to write file to directory " + strDir, ex);                
            }
        }
    }
}
