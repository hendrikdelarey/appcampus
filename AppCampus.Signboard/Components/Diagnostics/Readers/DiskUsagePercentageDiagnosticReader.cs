using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AppCampus.Signboard.Components.Diagnostics.Readers
{
    public class DiskUsagePercentageDiagnosticReader : IDiagnosticReader
    {
        public int ReadDiagnostic()
        {
            long total = 0;
            long available = 0;

            foreach (DriveInfo drive in DriveInfo.GetDrives().Where(d => d.IsReady))
            {
                total += drive.TotalSize;
                available += drive.AvailableFreeSpace;
            }

            return Convert.ToInt32((total - available) * 100 / total);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            Debug.WriteLine("DiskUsagePercentageDiagnosticReader disposed");
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
        }
    }
}