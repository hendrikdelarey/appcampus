using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCampus.Signboard.Models.QueryModels
{
    public class DiagnosticMetricModel
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public static DiagnosticMetricModel From(string name, int value) 
        {
            return new DiagnosticMetricModel()
            {
                Name = name,
                Value = value
            };
        }
    }
}
