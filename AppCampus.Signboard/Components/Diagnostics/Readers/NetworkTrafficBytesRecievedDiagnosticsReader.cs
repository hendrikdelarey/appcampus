using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCampus.Signboard.Components.Diagnostics.Readers
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
    public class NetworkTrafficBytesReceivedDiagnosticsReader : IDiagnosticReader
    {
        public NetworkTrafficBytesReceivedDiagnosticsReader() 
        {
        }

        public int ReadDiagnostic()
        {
            return (int)NetworkTrafficUtilities.Instance.GetBytesReceived();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
        public void Dispose()
        {
            // nothing to dispose
        }
    }
}
