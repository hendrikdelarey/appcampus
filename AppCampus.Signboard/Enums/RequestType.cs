using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCampus.Signboard.Enums
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
