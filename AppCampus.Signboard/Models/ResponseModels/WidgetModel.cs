using AppCampus.Signboard.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCampus.Signboard.Models
{
    public class WidgetModel
    {
        public string Type { get; set; }
        public WidgetPositionModel Position { get; set; }
        public List<ParameterModel> Parameters { get; set; }

        public string GetValueByType(ParameterType type)
        {
            return Parameters.FirstOrDefault(x => x.Definition == type.ToString()).Value;
        }
    }
}
