using AppCampus.Domain.Models.Entities;
using AppCampus.Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCampus.Domain.Interfaces.Components
{
    public interface IScreenshotComponent
    {
        bool HasScreenshot(Guid screenshotId);

        void StoreScreenshot(Screenshot screenshot);

        Screenshot Find(Guid screenshotId);
    }
}
