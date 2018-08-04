namespace FloatingRestServer.Common.Loggers
{
    public class RestServerLogger : LoggerCore
    {
        private static readonly ConsoleLogger ConsoleLogger = new ConsoleLogger();
        private static readonly FileLogger FileLogger = new FileLogger();

        public override void Log(LogEvent logEvent)
        {
            ConsoleLogger.Log(logEvent);
            FileLogger.Log(logEvent);
        }

        public void SetLogLevels(LogLevel console, LogLevel file)
        {
            ConsoleLogger.LogLevel = console;
            FileLogger.LogLevel = file;
        }
    }
}