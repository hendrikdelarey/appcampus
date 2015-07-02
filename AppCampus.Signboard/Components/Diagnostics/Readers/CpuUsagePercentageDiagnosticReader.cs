using System;
using System.Diagnostics;

namespace AppCampus.Signboard.Components.Diagnostics.Readers
{
    public class CpuUsagePercentageDiagnosticReader : IDiagnosticReader
    {
        private PerformanceCounter counter;

        protected PerformanceCounter Counter
        {
            get
            {
                if (counter == null)
                {
                    counter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                    counter.NextValue();
                }

                return counter;
            }
        }

        public int ReadDiagnostic()
        {
            return Convert.ToInt32(Counter.NextValue());
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                counter.Dispose();
                Debug.WriteLine("CpuUsagePercentageDiagnosticReader disposed");
            }
        }
    }
}