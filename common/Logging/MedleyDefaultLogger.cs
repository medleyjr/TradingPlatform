using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;


namespace Medley.Common.Logging
{
    public enum TLLogLevel
   {
       debug    = 1,
       info     = 2,
       warn     = 3,
       error    = 4
   }

    /// <summary>
    /// Default logger class.
    /// </summary>
    public sealed class MedleyDefaultLogger : IMedleyLogger
    {
        // Fields
        private string _logPath;
        private TLLogLevel m_logLevel = TLLogLevel.debug;

        public string LogFileName { get; set; }

        // Methods
        public MedleyDefaultLogger()
        {
            AddThreadId = true;
            this.InitLog();
        }


        #region IMedleyLogger Members

        public bool AddThreadId { get; set; }

        public IMedleyLogger CreateChildLogger(string name)
        {
            return this;
        }

        public void Debug(string message)
        {
            if (m_logLevel <= TLLogLevel.debug)
                WriteToLog(message);
        }

        public void Debug(string message, Exception exception)
        {
            if (m_logLevel <= TLLogLevel.debug)
            {
                WriteToLog(message);
                WriteException(exception);
            }
        }

        public void Error(string message)
        {
            if (m_logLevel <= TLLogLevel.error)
                WriteToLog(message);
        }

        public void Error(string message, Exception exception)
        {
            if (m_logLevel <= TLLogLevel.error)
            {
                WriteToLog(message);
                WriteException(exception);
            }
        }

        public void Info(string message)
        {
            if (m_logLevel <= TLLogLevel.info)
                WriteToLog(message);
        }

        public void Info(string message, Exception exception)
        {
            if (m_logLevel <= TLLogLevel.info)
            {
                WriteToLog(message);
                WriteException(exception);
            }
        }

        public void Warn(string message)
        {
            if (m_logLevel <= TLLogLevel.warn)
                WriteToLog(message);
        }

        public void Warn(string message, Exception exception)
        {
            if (m_logLevel <= TLLogLevel.warn)
            {
                WriteToLog(message);
                WriteException(exception);
            }
        }

        #endregion

        #region Helper Functions

        private void InitLog()
        {
            PreparLog();
            this.WriteToLog("----------------------- Log started -----------------");
        }

        private void PreparLog()
        {
            _logPath = Environment.CurrentDirectory + @"\Log";
            LogFileName = _logPath + @"\Log.txt";
            if (!Directory.Exists(_logPath))
            {
                Directory.CreateDirectory(_logPath);
            }
        }

        private void WriteToLog(string dataToLog)
        {
            StreamWriter writer = null;
            try
            {
                writer = File.AppendText(LogFileName);
            }
            catch
            {
                return;
            }

            lock (writer)
            {
                string threadId = " | ";
                if (AddThreadId)
                    threadId = string.Format(" | Thread Id = {0} | ", Thread.CurrentThread.ManagedThreadId);
                
                string str = DateTime.Now.ToString("dd/MM/yy | HH:mm:ss") + threadId + dataToLog + "\r\n";
                try
                {
                    writer.Write(str);
                    writer.Flush();
                }
                finally
                {
                    if (writer != null)
                    {
                        writer.Close();
                    }
                }
            }
        }

        private void WriteException(Exception ex)
        {
            WriteToLog(string.Concat(new object[] { " Exception: ", ex.Source, " whith error message: ", ex.Message, " Inner exception (", ex.InnerException, ")" }));
            WriteToLog(ex.StackTrace);
        }

        #endregion
    }
}
