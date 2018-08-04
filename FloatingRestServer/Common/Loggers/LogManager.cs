using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloatingRestServer.Common.Loggers
{
    public class LogManager
    {
        private static readonly Lazy<LogManager> _lazy = new Lazy<LogManager>(() => new LogManager());
        public static LogManager Instance => _lazy.Value;

        public RestServerLogger Logger { get; } = new RestServerLogger();
        private LogManager() { }
        
        public void SetLogLevels(LogLevel console, LogLevel file)
        {
            Logger.SetLogLevels(console, file);
        }
    }
}
