using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;
using System.Linq;

namespace AppCampus.Signboard.Components.Diagnostics.Readers
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    public class NetworkTrafficUtilities
    {
        private PerformanceCounter bytesSentPerformanceCounter;
        private PerformanceCounter bytesReceivedPerformanceCounter;
        private bool countersInitialized;
        private int pid;

        private static NetworkTrafficUtilities instance;
        public static NetworkTrafficUtilities Instance 
        {
            get 
            {
                if (instance == null) 
                {
                    instance = new NetworkTrafficUtilities();
                }

                return instance;
            }
        }

        private NetworkTrafficUtilities()
        {
            pid = Process.GetCurrentProcess().Id;
            TryToInitializeCounters();
        }

        private void TryToInitializeCounters()
        {
            if (!countersInitialized)
            {
                PerformanceCounterCategory category = new PerformanceCounterCategory(".NET CLR Networking 4.0.0.0");

                var instanceNames = category.GetInstanceNames().Where(i => i.Contains(string.Format("p{0}", pid)));

                if (instanceNames.Any())
                {
                    bytesSentPerformanceCounter = new PerformanceCounter();
                    bytesSentPerformanceCounter.CategoryName = ".NET CLR Networking 4.0.0.0";
                    bytesSentPerformanceCounter.CounterName = "Bytes Sent";
                    bytesSentPerformanceCounter.InstanceName = instanceNames.First();
                    bytesSentPerformanceCounter.ReadOnly = true;

                    bytesReceivedPerformanceCounter = new PerformanceCounter();
                    bytesReceivedPerformanceCounter.CategoryName = ".NET CLR Networking 4.0.0.0";
                    bytesReceivedPerformanceCounter.CounterName = "Bytes Received";
                    bytesReceivedPerformanceCounter.InstanceName = instanceNames.First();
                    bytesReceivedPerformanceCounter.ReadOnly = true;

                    countersInitialized = true;
                }
            }
        }

        public float GetBytesSent()
        {
            float bytesSent = 0;

            try
            {
                TryToInitializeCounters();
                bytesSent = bytesSentPerformanceCounter.RawValue;
            }
            catch (Exception) 
            {
                Logging.Logger.Instance.Write("GetBytesSent", Logging.LogLevel.Medium, "An exception occurred when trying to read how many bytes were sent");
            }

            return bytesSent;
        }

        public float GetBytesReceived()
        {
            float bytesSent = 0;

            try
            {
                TryToInitializeCounters();
                bytesSent = bytesReceivedPerformanceCounter.RawValue;
            }
            catch (Exception) 
            {
                Logging.Logger.Instance.Write("GetBytesReceived", Logging.LogLevel.Medium, "An exception occurred when trying to read how many bytes were recieved");
            }

            return bytesSent;
        }

        private static string GetInstanceName()
        {
            string instanceName = VersioningHelper.MakeVersionSafeName("Application.exe",
                                     ResourceScope.Machine, ResourceScope.AppDomain);

            return instanceName;
        }

    }
}
