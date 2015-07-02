using AppCampus.Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;

namespace AppCampus.Domain.Interfaces.Components
{
    public interface IDiagnosticsComponent
    {
        SignboardDiagnostics GetLatest(Guid signboardId);

        IEnumerable<SignboardDiagnostics> GetFrom(Guid signboardId, DateTime fromDate, int take);

        void Write(SignboardDiagnostics diagnostics);
    }
}