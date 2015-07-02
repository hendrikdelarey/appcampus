using AppCampus.Domain.Interfaces.Components;
using AppCampus.Domain.Models.Entities;
using AppCampus.Domain.Models.ValueObjects;
using AppCampus.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCampus.Infrastructure.Components
{
    public class ScreenshotComponent : IScreenshotComponent
    {
        public bool HasScreenshot(Guid screenshotId) 
        {
            using (var context = new AppCampusContext())
            {
                var screenshotTable = context.Screenshots.FirstOrDefault(x => x.Id == screenshotId);

                if (screenshotTable == null)
                {
                    return false;
                }
            }

            return true;
        }

        public Screenshot Find(Guid screenshotId) 
        {
            using (var context = new AppCampusContext())
            {
                var screenshotTable = context.Screenshots.First(x => x.Id == screenshotId);
                return Screenshot.From(screenshotTable.Id, screenshotTable.Base64ImageString, screenshotTable.DeviceId, screenshotTable.CreatedDate);
            }
        }

        public void StoreScreenshot(Screenshot screenshot)
        {
            using (var context = new AppCampusContext())
            {
                context.Screenshots.Add(
                    new ScreenshotTable()
                    {
                        Id = screenshot.Id,
                        DeviceId = screenshot.DeviceId,
                        CreatedDate = DateTime.UtcNow,
                        Base64ImageString = screenshot.Base64ImageString
                    });
                context.SaveChanges();
            }
        }
    }
}
