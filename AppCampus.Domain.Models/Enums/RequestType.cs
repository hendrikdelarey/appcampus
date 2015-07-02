using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCampus.Domain.Models.Enums
{
    public enum RequestType
    {
        Undefined,
        RestartDevice,
        ShowScreensaver,
        SoftwareUpdate,
        FontFactorUpdate,
        TakeScreenshot,
        RetrieveLog
    }
}
