using System;

namespace FloatingRestServer.Common.Loggers
{
    public class ConsoleLogger : ILogger
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Trace;

        public void Log(LogEvent logEvent)
        {
            if (LogLevel > logEvent.LogLevel)
            {
                return;
            }

            switch (logEvent.LogLevel)
            {
                case LogLevel.Trace:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case LogLevel.Debug:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case LogLevel.Info:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case LogLevel.Warn:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogLevel.Error:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case LogLevel.Fatal:
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
            }

            Console.WriteLine(logEvent.ToString());
        }

        public void Trace(object obj)
        {
            Log(new LogEvent(LogLevel.Trace, obj.ToString(), DateTime.Now));
        }

        public void Trace(string message)
        {
            Log(new LogEvent(LogLevel.Trace, message, DateTime.Now));
        }

        public void Trace(string message, Exception e)
        {
            Log(new LogEvent(LogLevel.Trace, ExceptionToString(message, e), DateTime.Now));
        }

        public void Debug(object obj)
        {
            Log(new LogEvent(LogLevel.Debug, obj.ToString(), DateTime.Now));
        }

        public void Debug(string message)
        {
            Log(new LogEvent(LogLevel.Debug, message, DateTime.Now));
        }

        public void Debug(string message, Exception e)
        {
            Log(new LogEvent(LogLevel.Debug, ExceptionToString(message, e), DateTime.Now));
        }

        public void Info(object obj)
        {
            Log(new LogEvent(LogLevel.Info, obj.ToString(), DateTime.Now));
        }

        public void Info(string message)
        {
            Log(new LogEvent(LogLevel.Info, message, DateTime.Now));
        }

        public void Info(string message, Exception e)
        {
            Log(new LogEvent(LogLevel.Info, ExceptionToString(message, e), DateTime.Now));
        }

        public void Warn(object obj)
        {
            Log(new LogEvent(LogLevel.Warn, obj.ToString(), DateTime.Now));
        }

        public void Warn(string message)
        {
            Log(new LogEvent(LogLevel.Warn, message, DateTime.Now));
        }

        public void Warn(string message, Exception e)
        {
            Log(new LogEvent(LogLevel.Warn, ExceptionToString(message, e), DateTime.Now));
        }

        public void Error(object obj)
        {
            Log(new LogEvent(LogLevel.Error, obj.ToString(), DateTime.Now));
        }

        public void Error(string message)
        {
            Log(new LogEvent(LogLevel.Error, message, DateTime.Now));
        }

        public void Error(string message, Exception e)
        {
            Log(new LogEvent(LogLevel.Error, ExceptionToString(message, e), DateTime.Now));
        }

        public void Fatal(object obj)
        {
            Log(new LogEvent(LogLevel.Fatal, obj.ToString(), DateTime.Now));
        }

        public void Fatal(string message)
        {
            Log(new LogEvent(LogLevel.Fatal, message, DateTime.Now));
        }

        public void Fatal(string message, Exception e)
        {
            Log(new LogEvent(LogLevel.Fatal, ExceptionToString(message, e), DateTime.Now));
        }

        private string ExceptionToString(string message, Exception e)
        {
            return $"{message} {e.Message} {e.StackTrace}";
        }
    }
}
