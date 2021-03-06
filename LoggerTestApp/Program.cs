﻿using System;
using FloatingRestServer.Common.Loggers;

namespace LoggerTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            RestServerLogger logger = LogManager.Instance.Logger;
            logger.SetLogLevels(LogLevel.Info, LogLevel.Fatal);
            logger.Trace(1);
            logger.Trace("Tracetest");
            logger.Trace("test", new NullReferenceException());

            logger.Debug(1);
            logger.Debug("test");
            logger.Debug("test", new NullReferenceException());

            logger.Info(1);
            logger.Info("test");
            logger.Info("test", new NullReferenceException());

            logger.Warn(1);
            logger.Warn("test");
            logger.Warn("test", new NullReferenceException());

            logger.Error(1);
            logger.Error("test");
            logger.Error("test", new NullReferenceException());

            logger.Fatal(1);
            logger.Fatal("test");
            logger.Fatal("test", new NullReferenceException());

            Console.ReadLine();
        }
    }
}
