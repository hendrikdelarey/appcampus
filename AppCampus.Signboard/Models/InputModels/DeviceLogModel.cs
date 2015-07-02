using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCampus.Signboard.Models.InputModels
{
    public class DeviceLogModel
    {
        public string FileName { get; set; }

        private DeviceLogModel(string filename) 
        {
            FileName = filename;
        }

        public static DeviceLogModel From(string filename) 
        {
            return new DeviceLogModel(filename);
        }
    }
}
