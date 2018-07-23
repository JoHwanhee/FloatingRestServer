using System;

namespace FloatingRestServer.Common.Loggers
{
    public interface ILogger
    {
        LogLevel LogLevel { get; set; }
        void Log(LogEvent logEvent);
        void Trace(object obj);
        void Trace(string message);
        void Trace(string message, Exception e);
        void Debug(object obj);
        void Debug(string message);
        void Debug(string message, Exception e);
        void Info(object obj);
        void Info(string message);
        void Info(string message, Exception e);
        void Warn(object obj);
        void Warn(string message);
        void Warn(string message, Exception e);
        void Error(object obj);
        void Error(string message);
        void Error(string message, Exception e);
        void Fatal(object obj);
        void Fatal(string message);
        void Fatal(string message, Exception e);
    }
}
