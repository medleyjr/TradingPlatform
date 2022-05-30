using System;

namespace Medley.Common.Logging
{
    /// <summary>
    /// Interface for Logging.
    /// </summary>
    public interface IMedleyLogger
    {
        IMedleyLogger CreateChildLogger(string name);
        void Debug(string message);
        void Debug(string message, Exception exception);
        void Error(string message);
        void Error(string message, Exception exception);
        void Info(string message);
        void Info(string message, Exception exception);
        void Warn(string message);
        void Warn(string message, Exception exception);

        bool AddThreadId { get; set; }
    }
}
