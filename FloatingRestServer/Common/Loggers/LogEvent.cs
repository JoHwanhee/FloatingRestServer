using System;

namespace FloatingRestServer.Common.Loggers
{
    public struct LogEvent
    {
        public LogLevel LogLevel;
        public string Message;
        public DateTime EventTime;
        public string DateFormat;

        public LogEvent(LogLevel logLevel, string message, DateTime eventTime, string dateFormat = "HH:mm:ss")
        {
            LogLevel = logLevel;
            Message = message;
            EventTime = eventTime;
            DateFormat = dateFormat;
        }

        public override string ToString()
        {
            return $"[{EventTime.ToString(DateFormat)}] [{LogLevel.ToString()}] {Message}";
        }
    }
}