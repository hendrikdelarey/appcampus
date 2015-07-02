using AppCampus.Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCampus.Domain.Interfaces.Components
{
    public interface IDrumbleComponent
    {
        IEnumerable<Stop> GetStops();

        IEnumerable<Operator> GetOperators();
    }
}
