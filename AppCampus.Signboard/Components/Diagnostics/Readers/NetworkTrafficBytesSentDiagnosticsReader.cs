using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCampus.Signboard.Components.Diagnostics.Readers
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
    public class NetworkTrafficBytesSentDiagnosticsReader : IDiagnosticReader
    {
        public NetworkTrafficBytesSentDiagnosticsReader() 
        {
        }

        public int ReadDiagnostic()
        {
            return (int)NetworkTrafficUtilities.Instance.GetBytesSent();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
        public void Dispose()
        {
            // nothing to dispose
        }
    }
}
