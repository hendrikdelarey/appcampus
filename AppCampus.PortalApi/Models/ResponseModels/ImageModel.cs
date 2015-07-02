using System;

namespace AppCampus.PortalApi.Models.ResponseModels
{
    /// <summary>
    /// The Image response model.
    /// </summary>
    public class ImageModel
    {
        /// <summary>
        /// The identifier of the image.
        /// </summary>
        public Guid ImageId { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// The Base64 image string.
        /// </summary>
        public string Base64Image { get; set; }

        public static ImageModel From(Guid imageId, string base64Image, string name)
        {
            return new ImageModel()
            {
                ImageId = imageId,
                Base64Image = base64Image,
                Name = name
            };
        }

        public static ImageModel FromExcludingBase64(Guid imageId, string name)
        {
            return new ImageModel()
            {
                ImageId = imageId,
                Base64Image = null,
                Name = name
            };
        }
    }
}