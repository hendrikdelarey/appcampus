using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppCampus.SignboardApi.Models.ResponseModels
{
    /// <summary>
    /// The Image Resonse Model
    /// </summary>
    public class ImageResponseModel
    {

        /// <summary>
        /// The Base 64 Image String representing the image.
        /// </summary>
        public string Base64Image { get; set; }

        public static ImageResponseModel From(string base64Image) 
        {
            return new ImageResponseModel()
            {
                Base64Image = base64Image
            };
        }
    }
}