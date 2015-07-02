using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drumble.DomainDrivenArchitecture.Domain.Models;

namespace AppCampus.Domain.Models.Entities
{
    public class Screenshot : DomainEntity<Guid>, IAggregateRoot
    {
        public string Base64ImageString { get; private set; }

        public DateTime CreatedDate { get; private set; }

        public Guid DeviceId { get; private set; }

        private Screenshot(Guid screenshotId, string base64image, Guid deviceId) :
            base(screenshotId)
        {
            if (String.IsNullOrEmpty(base64image))
            {
                throw new ArgumentNullException("base64image", "Not a valid Base64 Image String");
            }

            Base64ImageString = base64image;
            DeviceId = deviceId;
        }

        private Screenshot(Guid screenshotId, string base64image, Guid deviceId, DateTime dateTime) :
            base(screenshotId)
        {
            if (String.IsNullOrEmpty(base64image))
            {
                throw new ArgumentNullException("base64image", "Not a valid Base64 Image String");
            }

            Base64ImageString = base64image;
            DeviceId = deviceId;
            CreatedDate = dateTime;
        }

        public static Screenshot From(Guid screenshotId, string base64Image, Guid deviceId)
        {
            return new Screenshot(screenshotId, base64Image, deviceId);
        }

        public static Screenshot From(Guid screenshotId, string base64Image, Guid deviceId, DateTime dateTime)
        {
            return new Screenshot(screenshotId, base64Image, deviceId, dateTime);
        }
    }
}
