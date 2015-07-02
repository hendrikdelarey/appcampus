using AppCampus.Signboard.Components.Logging;
using System.Diagnostics;
using System.Security;

namespace AppCampus.Signboard.Components
{
    public class HardwareComponent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands")]
        internal static bool Shutdown()
        {
            Logger.Instance.Write("Shutdown", LogLevel.Critical, "Shutting down the  device");

            try
            {
                Process.Start("shutdown", "/s /t 0");
                return true;
            }
            catch (SecurityException)
            {
                return false;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands")]
        internal static bool Restart()
        {
            Logger.Instance.Write("Restart", LogLevel.Low, "Restarting the device");

            try
            {
#if DEBUG
#else
                Process.Start("shutdown", "/r /t 0");
#endif
                return true;
            }
            catch (SecurityException)
            {
                return false;
            }
        }
    }
}