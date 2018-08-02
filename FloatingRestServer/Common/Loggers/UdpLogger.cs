using System;

namespace FloatingRestServer.Common.Loggers
{
    public class UdpLogger : ILogger
    {
        // todo : use syslog protocol

        public LogLevel LogLevel { get; set; }
        public void Log(LogEvent logEvent)
        {
            throw new NotImplementedException();
        }

        public void Trace(object obj)
        {
            throw new NotImplementedException();
        }

        public void Trace(string message)
        {
            throw new NotImplementedException();
        }

        public void Trace(string message, Exception e)
        {
            throw new NotImplementedException();
        }

        public void Debug(object obj)
        {
            throw new NotImplementedException();
        }

        public void Debug(string message)
        {
            throw new NotImplementedException();
        }

        public void Debug(string message, Exception e)
        {
            throw new NotImplementedException();
        }

        public void Info(object obj)
        {
            throw new NotImplementedException();
        }

        public void Info(string message)
        {
            throw new NotImplementedException();
        }

        public void Info(string message, Exception e)
        {
            throw new NotImplementedException();
        }

        public void Warn(object obj)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message, Exception e)
        {
            throw new NotImplementedException();
        }

        public void Error(object obj)
        {
            throw new NotImplementedException();
        }

        public void Error(string message)
        {
            throw new NotImplementedException();
        }

        public void Error(string message, Exception e)
        {
            throw new NotImplementedException();
        }

        public void Fatal(object obj)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string message)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string message, Exception e)
        {
            throw new NotImplementedException();
        }
    }
}
