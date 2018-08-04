using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FloatingRestServer.Common.Loggers
{
    public class FileLogger : LoggerCore
    {
        private bool _isStarted;
        private readonly ConcurrentQueue<LogEvent> _logs = new ConcurrentQueue<LogEvent>();
        private readonly object _arrayLockObject = new object();
        
        public string LogFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs", $"{DateTime.Now.ToShortDateString()}.log");
        private readonly Thread _writeWoker = null;


        public FileLogger()
        {
            CreateDirectory(LogFullName);
            _writeWoker = new Thread(WriteLogWorker);
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

        private void Start()
        {
            _isStarted = true;
            _writeWoker.IsBackground = true;
            _writeWoker.Start();
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

        public override void Log(LogEvent logEvent)
        {
            if (LogLevel <= logEvent.LogLevel)
            {
                Task.Factory.StartNew(() =>
                {
                    lock (_arrayLockObject)
                    {
                        _logs.Enqueue(logEvent);
                    }
                });
            }
        }
    }
}
