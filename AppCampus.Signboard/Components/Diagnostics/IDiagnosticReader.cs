using System;

namespace AppCampus.Signboard.Components.Diagnostics
{
    public interface IDiagnosticReader : IDisposable
    {
        int ReadDiagnostic();
    }
}