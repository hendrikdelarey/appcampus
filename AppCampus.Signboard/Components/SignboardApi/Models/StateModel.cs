using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCampus.Signboard.Components.SignboardApi.Models
{
    public class StateModel
    {
        public string State { get; set; }

        public DateTime ChangeDate { get; set; }

        public string Comment { get; set; }
    }
}
