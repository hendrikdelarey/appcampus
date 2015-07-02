using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppCampus.SignboardApi.Models.InputModels
{
    public class ScreenshotInputModel
    {
        public Guid ScreenshotId { get; set; }
        public string Base64ImageString { get; set; }
    }
}