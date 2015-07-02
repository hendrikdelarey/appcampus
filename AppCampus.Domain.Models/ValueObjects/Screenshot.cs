//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AppCampus.Domain.Models.ValueObjects
//{
//    public class Screenshot
//    {
//        public Guid ScreenshotId { get; private set; }
//        public string Base64ImageString { get; private set; }

//        public DateTime CreatedDate { get; private set; }

//        public Guid DeviceId { get; private set; }

//        private Screenshot(Guid screenshotId, string base64image, Guid deviceId) 
//        {
//            if (String.IsNullOrEmpty(base64image)) 
//            {
//                throw new ArgumentNullException("base64image", "Not a valid Base64 Image String");
//            }

//            ScreenshotId = screenshotId;
//            Base64ImageString = base64image;
//            DeviceId = deviceId;
//        }

//        private Screenshot(Guid screenshotId, string base64image, Guid deviceId, DateTime dateTime)
//        {
//            if (String.IsNullOrEmpty(base64image))
//            {
//                throw new ArgumentNullException("base64image", "Not a valid Base64 Image String");
//            }

//            ScreenshotId = screenshotId;
//            Base64ImageString = base64image;
//            DeviceId = deviceId;
//            CreatedDate = dateTime;
//        }

//        public static Screenshot From(Guid screenshotId, string base64image, Guid deviceId) 
//        {
//            return new Screenshot(screenshotId, base64image, deviceId);
//        }

//        public static Screenshot From(Guid screenshotId, string base64image, Guid deviceId, DateTime dateTime)
//        {
//            return new Screenshot(screenshotId, base64image, deviceId, dateTime);
//        }
//    }
//}
