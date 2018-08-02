using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FloatingRestServer.Common.Loggers
{
    public class FileLogger
    {
        public LogLevel LogLevel { get; set; }
        private bool _isStarted;
        private readonly ConcurrentQueue<LogEvent> _logs = new ConcurrentQueue<LogEvent>();
        private readonly object _arrayLockObject = new object();

        public string LogFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs", $"{DateTime.Now.ToShortDateString()}.log");

        public FileLogger()
        {
            CreateDirectory(LogFullName);
            Start();
        }

        private void CreateDirectory(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return;
            }

            var directoryName = Path.GetDirectoryName(fileName);
            if (string.IsNullOrWhiteSpace(directoryName))
            {
                return;
            }

            Directory.CreateDirectory(directoryName);
        }

        public void Start()
        {
            _isStarted = true;
            Task.Factory.StartNew(WriteLogWorker);
        }

        public void Stop()
        {
            _isStarted = false;
        }

        public void WriteLogWorker()
        {
            while (_isStarted)
            {
                lock (_arrayLockObject)
                {
                    if (_logs.TryDequeue(out var logEvent))
                    {
                        using (StreamWriter writer = new StreamWriter(LogFullName, true))
                        {
                            writer.WriteLine(logEvent.ToString());
                        }
                    }
                }
            }
        }

        public void Log(LogEvent logEvent)
        {
            Task.Factory.StartNew(() =>
            {
                lock (_arrayLockObject)
                {
                    _logs.Enqueue(logEvent);
                }
            });
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
