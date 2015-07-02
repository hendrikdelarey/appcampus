using AppCampus.Signboard.Components.Logging;
using AppCampus.Signboard.Components.ScreenCapture;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AppCampus.Signboard.Helpers
{
    public static class ImageHelper
    {
        public static Image ConvertBase64StringToImage(String imageString) 
        {
            if (imageString == null || imageString.Length == 0) 
            {
                return new Image();
            }

            byte[] binaryData = Convert.FromBase64String(imageString);
            
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new MemoryStream(binaryData);
            bitmapImage.EndInit();
            
            Image image = new Image();
            image.Source = bitmapImage;

            return image;
        }
    }
}
