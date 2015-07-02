using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCampus.Signboard.Models.InputModels
{
    public class ScreenshotModel
    {
        public Guid ScreenshotId { get; set; }

        public string Base64ImageString { get; set; }

        private ScreenshotModel(Guid screenshotId, string base64ImageString) 
        {
            ScreenshotId = screenshotId;
            Base64ImageString = base64ImageString;
        }

        public static ScreenshotModel From(Guid screenshotId, string base64ImageString) 
        {
            return new ScreenshotModel(screenshotId, base64ImageString);
        }
    }
}
