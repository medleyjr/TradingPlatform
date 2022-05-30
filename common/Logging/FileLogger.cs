using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace Medley.Common.Logging
{
    /// <summary>
    /// Log each data string as a seperate file in a folder.
    /// </summary>
    public class FileLogger
    {
        private string m_LogDirectory = @".\";
        private string m_Ext = ".xml";
        private int nCounter = 0;
        private int m_nExpiryPeriod = 0;

        public FileLogger()
            : this(@".\", ".xml", 0)
        {

        }

        public FileLogger(string directory, string ext, int expiryPeriod)
        {
            m_LogDirectory = directory;
            if (!Directory.Exists(m_LogDirectory))
                Directory.CreateDirectory(m_LogDirectory);

            if (!m_LogDirectory.EndsWith(@"\"))
                m_LogDirectory += @"\";

            m_Ext = ext;
            m_nExpiryPeriod = expiryPeriod;

            BackupCurrentLogs();
            DoCleanUp();
        }

        public void BackupCurrentLogs()
        {
            try
            {
                string strBackFolder = m_LogDirectory + DateTime.Now.ToString("ddMMyyyyHHmmss") + @"\";
                if(!MoveFilesToDir(m_LogDirectory, strBackFolder, "*" + m_Ext))
                {
                    MedleyLogger.Instance.Error("Failed to backup log files");
                }                
            }
            catch (Exception ex)
            {
                MedleyLogger.Instance.Error("Failed to backup log files", ex);
            }
        }

        public void DoCleanUp()
        {
            if (m_nExpiryPeriod == 0)
                return;

            try
            {
                foreach (string strDir in Directory.GetDirectories(m_LogDirectory))
                {
                    TimeSpan timeDiff = DateTime.Now - Directory.GetLastWriteTime(strDir);
                    if (timeDiff.Days >= m_nExpiryPeriod)
                        Directory.Delete(strDir, true);
                }
            }
            catch (Exception ex)
            {
                MedleyLogger.Instance.Error("Failed to do cleanup of log files", ex);
            }
        }

        public void LogDataFile(byte[] data, string name)
        {
            IncreaseCounter();
            string strPath = GetPath(name); //string.Format("{0}{1:ddMMyyyyHHmmssFFF}.dat", m_LogDirectory, DateTime.Now);
            File.WriteAllBytes(strPath, data);
        }

        public void LogTextFile(string text, string name)
        {
            IncreaseCounter();
            string strPath = GetPath(name);
            File.WriteAllText(strPath, text);
        }

        private void IncreaseCounter()
        {
            if (nCounter == 99999)
                nCounter = 1;
            else
                nCounter++;
        }

        private string GetPath(string name)
        {
            return string.Format("{0}{1:00000} - {2}" + m_Ext, m_LogDirectory, nCounter, name);
        }

        private bool MoveFilesToDir(string sourceDir, string destDir, string searchOptions)
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
            catch (Exception ex)
            {
                MedleyLogger.Instance.Error("Failed to move files", ex);
                return false;
            }

            return true;
        }

    }
}
