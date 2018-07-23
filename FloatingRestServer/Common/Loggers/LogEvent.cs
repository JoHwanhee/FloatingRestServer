using System;

namespace FloatingRestServer.Common.Loggers
{
    public struct LogEvent
    {
        public LogLevel LogLevel;
        public string Message;
        public DateTime EventTime;
        public string DateFormat;

        public LogEvent(LogLevel logLevel, string message, DateTime eventTime, string dateFormat = "yyyy MM dd hh:mm:ss tt")
        {
            LogLevel = logLevel;
            Message = message;
            EventTime = eventTime;
            DateFormat = dateFormat;
        }

        public override string ToString()
        {
            return $"[{LogLevel.ToString()}] [{EventTime.ToString(DateFormat)}] {Message}";
        }
    }
}