using System;
using System.Reflection;

namespace AppCampus.Domain.Interfaces.Components
{
    public interface ILoggingComponent
    {
        void LogTrace(MethodBase context, string message);

        void LogDebug(MethodBase context, string message);

        void LogInfo(MethodBase context, string message);

        void LogWarning(MethodBase context, string message);

        void LogError(MethodBase context, string message);

        void LogError(MethodBase context, string message, Exception ex);

        void LogFatal(MethodBase context, string message);

        void LogFatal(MethodBase context, string message, Exception ex);
    }
}