using AppCampus.Domain.Interfaces.Components;
using System;
using System.Reflection;
using Drumble.Logging.Loggers;

namespace AppCampus.Infrastructure.Components
{
    public class LoggingComponent : ILoggingComponent
    {
        private static string applicationLogger = "Application";

        public LoggingComponent()
        {
        }

        public static LoggingComponent Instance
        {
            get
            {
                return new LoggingComponent();
            }
        }

        public void LogTrace(MethodBase context, string message)
        {
            ApplicationLogger.GetLogger(applicationLogger).LogTrace(context, message);
        }

        public void LogDebug(MethodBase context, string message)
        {
            ApplicationLogger.GetLogger(applicationLogger).LogDebug(context, message);
        }

        public void LogInfo(MethodBase context, string message)
        {
            ApplicationLogger.GetLogger(applicationLogger).LogInfo(context, message);
        }

        public void LogWarning(MethodBase context, string message)
        {
            ApplicationLogger.GetLogger(applicationLogger).LogWarning(context, message);
        }

        public void LogError(MethodBase context, string message)
        {
            ApplicationLogger.GetLogger(applicationLogger).LogError(context, message);
        }

        public void LogError(MethodBase context, string message, Exception ex)
        {
            ApplicationLogger.GetLogger(applicationLogger).LogError(context, message, ex);
        }

        public void LogFatal(MethodBase context, string message)
        {
            ApplicationLogger.GetLogger(applicationLogger).LogFatal(context, message);
        }

        public void LogFatal(MethodBase context, string message, Exception ex)
        {
            ApplicationLogger.GetLogger(applicationLogger).LogFatal(context, message, ex);
        }
    }
}