using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

using log4net;
using log4net.Config;

using Medley.Common.Logging;

//[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Log4Net.config", Watch = true)]

namespace Tradelink.Common.Logging
{
    public class Log4NetLogger : IMedleyLogger
    {
        private static ILog logger = null;

        //log4net config will be taken from the app.config file
        public Log4NetLogger()
        {            
            AddThreadId = true;
            logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

        //log4net config will be taken from [loggerName].log4net.config
        public Log4NetLogger(string loggerName)
        {
            FileInfo fileInfo = new FileInfo(loggerName + ".log4net.config");
            log4net.Config.XmlConfigurator.ConfigureAndWatch(fileInfo);
            AddThreadId = true;
            logger = LogManager.GetLogger(loggerName);
        }

        public IMedleyLogger CreateChildLogger(string name)
        {
            throw new NotImplementedException();
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Debug(string message, Exception exception)
        {
            logger.Debug(message, exception);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Error(string message, Exception exception)
        {
            logger.Error(message, exception);
        }

        public void Info(string message)
        {
            logger.Info(message);
        }

        public void Info(string message, Exception exception)
        {
            logger.Info(message, exception);
        }

        public void Warn(string message)
        {
            logger.Warn(message);
        }

        public void Warn(string message, Exception exception)
        {
            logger.Warn(message, exception);
        }

        public bool AddThreadId { get; set; }

    }
}
